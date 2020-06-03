using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using DeLozerkermisVrienden.Organizer.API.Models;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using DeLozerkermisVrienden.Organizer.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/inschrijvingen")]
    public class InschrijvingenController : ControllerBase
    {
        // Controle betaalmethode / Evenement / Inschrijvingsstatus / Lid
        private readonly IBetaalmethodeRepository _betaalmethodeRepository;
        private readonly IInschrijvingsstatusRepository _inschrijvingsstatusRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly ILidRepository _lidRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly IMailing _mailing;
        private readonly IMapper _mapper;

        public InschrijvingenController(IBetaalmethodeRepository betaalmethodeRepository, IBetaaltransactieRepository betaaltransactieRepository, IInschrijvingsstatusRepository inschrijvingsstatusRepository, IEvenementRepository evenementRepository, ILidRepository lidRepository, IInschrijvingRepository inschrijvingRepository, IMailing mailing, IMapper mapper)
        {
            _betaalmethodeRepository = betaalmethodeRepository ?? throw new ArgumentNullException(nameof(betaalmethodeRepository));
            _inschrijvingsstatusRepository = inschrijvingsstatusRepository ?? throw new ArgumentNullException(nameof(inschrijvingsstatusRepository));
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
            _lidRepository = lidRepository ?? throw new ArgumentNullException(nameof(lidRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _mailing = mailing ?? throw new ArgumentNullException(nameof(mailing));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet(Name = "GetInschrijvingen")]
        public ActionResult<IEnumerable<InschrijvingVoorRaadpleegDto>> GetInschrijvingen([FromQuery] InschrijvingenResourceParameters inschrijvingenResourceParameters)
        {
            var inschrijvingenVanRepo = _inschrijvingRepository.GetInschrijvingen(inschrijvingenResourceParameters);
            return Ok(_mapper.Map<IEnumerable<InschrijvingVoorRaadpleegDto>>(inschrijvingenVanRepo));
        }

        [Authorize(Roles = "Administrator")]
        [HttpHead("{inschrijvingsId}")]
        [HttpGet("{inschrijvingsId}", Name = "GetInschrijving")]
        public IActionResult GetInschrijving(Guid inschrijvingsId)
        {
            var inschrijvingVanRepo = _inschrijvingRepository.GetInschrijving(inschrijvingsId);
            if (inschrijvingVanRepo == null)
            {
                return NotFound($"Inschrijving '{inschrijvingsId}' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingVoorRaadpleegDto>(inschrijvingVanRepo));
        }

        [Authorize(Roles = "Standhouder, Administrator")]
        [HttpHead("{inschrijvingsId}/status")]
        [HttpGet("{inschrijvingsId}/status", Name = "GetInschrijvingVoorStatus")]
        public IActionResult GetInschrijvingVoorStatus(Guid inschrijvingsId)
        {
            var inschrijvingVanRepo = _inschrijvingRepository.GetInschrijving(inschrijvingsId);
            if (inschrijvingVanRepo == null)
            {
                return NotFound($"Inschrijving '{inschrijvingsId}' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingVoorRaadpleegStatusDto>(inschrijvingVanRepo));
        }


        [AllowAnonymous]
        [HttpPost(Name = "ToevoegenInschrijving")]
        public ActionResult<InschrijvingVoorRaadpleegDto> ToevoegenInschrijving([FromBody]InschrijvingVoorAanmaakDto inschrijving)
        {
            var inschrijvingEntity = _mapper.Map<Entities.Inschrijving>(inschrijving);
            inschrijvingEntity.DatumInschrijving = DateTime.UtcNow.AddHours(2);

            // === START Controle voor manipulatie === // // === EINDE Controle voor manipulatie === //
            if (!_betaalmethodeRepository.BestaatBetaalmethode(inschrijvingEntity.BetaalmethodeId.Value))
            {
                return NotFound($"Betaalmethode '{inschrijvingEntity.BetaalmethodeId.Value}' niet gevonden.");
            }

            if (!_inschrijvingsstatusRepository.BestaatInschrijvingsstatus(inschrijvingEntity.InschrijvingsstatusId.Value))
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingEntity.InschrijvingsstatusId.Value}' niet gevonden.");
            }

            if (!_evenementRepository.BestaatEvenement(inschrijvingEntity.EvenementId.Value))
            {
                return NotFound($"Evenement '{inschrijvingEntity.EvenementId.Value}' niet gevonden.");
            }

            if (inschrijvingEntity.LidId.HasValue)
            {
                if (!_lidRepository.BestaatLid(inschrijvingEntity.LidId.Value))
                {
                    return NotFound($"Lid '{inschrijvingEntity.LidId.Value}' niet gevonden.");
                }
            }

            if (inschrijvingEntity.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland().Id)
            {
                if (string.IsNullOrWhiteSpace(inschrijvingEntity.Standnummer))
                {
                    return Conflict($"Bij het plannen van een inschrijving moet een standnummer worden opgegeven.");
                }
            }

            if (inschrijvingEntity.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Afgekeurd().Id)
            {
                if (string.IsNullOrWhiteSpace(inschrijvingEntity.RedenAfkeuring))
                {
                    return Conflict($"Bij het afkeuren van een inschrijving moet een reden tot afkeuring worden opgegeven.");
                }
            }

            if (inschrijvingEntity.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Goedgekeurd().Id | inschrijvingEntity.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland().Id)
            {
                if (inschrijvingEntity.BetaalmethodeId.Value == _betaalmethodeRepository.GetBetaalmethode_Overschrijving().Id)
                {
                    if(string.IsNullOrWhiteSpace(inschrijvingEntity.GestructureerdeMededeling))
                    {
                        inschrijvingEntity.GestructureerdeMededeling = GestructureerdeMededeling.GetNieuweGestructureerdeMededeling(_inschrijvingRepository);
                    }
                }

                if (string.IsNullOrWhiteSpace(inschrijvingEntity.QRCode))
                {
                    inschrijvingEntity.QRCode = QRCode.GetNieuweQRCode();
                }
            }
            // === EINDE Controle voor manipulatie === //

            // === START null waarden wegwerken === //
            if (!inschrijvingEntity.AantalWagens.HasValue)
            {
                inschrijvingEntity.AantalWagens = 0;
            }
            if (!inschrijvingEntity.AantalAanhangwagens.HasValue)
            {
                inschrijvingEntity.AantalAanhangwagens = 0;
            }
            if (!inschrijvingEntity.AantalMobilhomes.HasValue)
            {
                inschrijvingEntity.AantalMobilhomes = 0;
            }
            // === EINDE null waarden wegwerken === //



            _inschrijvingRepository.ToevoegenInschrijving(inschrijvingEntity);
            _inschrijvingRepository.Opslaan();

            if (inschrijvingEntity.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Aangevraagd().Id)
            {
                _mailing.VerstuurMail_NieuweAanvraag(inschrijvingEntity);
            }

            var inschrijvingTeRetourneren = _mapper.Map<InschrijvingVoorRaadpleegDto>(inschrijvingEntity);
            return CreatedAtRoute("GetInschrijving", new { inschrijvingsId = inschrijvingTeRetourneren.Id }, inschrijvingTeRetourneren);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{inschrijvingsId}", Name = "VolledigeUpdateInschrijving")]
        public IActionResult VolledigeUpdateInschrijving(Guid inschrijvingsId, [FromBody]InschrijvingVoorUpdateDto inschrijving)
        {
            var inschrijvingVanRepo = _inschrijvingRepository.GetInschrijving(inschrijvingsId);
            if (inschrijvingVanRepo == null)
            {
                return NotFound($"Inschrijving '{inschrijvingsId}' niet gevonden.");
            }

            _mapper.Map(inschrijving, inschrijvingVanRepo);

            // === START Controle voor manipulatie === // // === EINDE Controle voor manipulatie === //
            if (!_betaalmethodeRepository.BestaatBetaalmethode(inschrijvingVanRepo.BetaalmethodeId.Value))
            {
                return NotFound($"Betaalmethode '{inschrijvingVanRepo.BetaalmethodeId.Value}' niet gevonden.");
            }

            if (!_inschrijvingsstatusRepository.BestaatInschrijvingsstatus(inschrijvingVanRepo.InschrijvingsstatusId.Value))
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingVanRepo.InschrijvingsstatusId.Value}' niet gevonden.");
            }

            if (!_evenementRepository.BestaatEvenement(inschrijvingVanRepo.EvenementId.Value))
            {
                return NotFound($"Evenement '{inschrijvingVanRepo.EvenementId.Value}' niet gevonden.");
            }

            if (inschrijvingVanRepo.LidId.HasValue)
            {
                if (!_lidRepository.BestaatLid(inschrijvingVanRepo.LidId.Value))
                {
                    return NotFound($"Lid '{inschrijvingVanRepo.LidId.Value}' niet gevonden.");
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland().Id)
            {
                if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.Standnummer))
                {
                    return Conflict($"Bij het plannen van een inschrijving moet een standnummer worden opgegeven.");
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Afgekeurd().Id)
            {
                if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.RedenAfkeuring))
                {
                    return Conflict($"Bij het afkeuren van een inschrijving moet een reden tot afkeuring worden opgegeven.");
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Goedgekeurd().Id | inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland().Id)
            {
                if (inschrijvingVanRepo.BetaalmethodeId.Value == _betaalmethodeRepository.GetBetaalmethode_Overschrijving().Id)
                {
                    if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.GestructureerdeMededeling))
                    {
                        inschrijvingVanRepo.GestructureerdeMededeling = GestructureerdeMededeling.GetNieuweGestructureerdeMededeling(_inschrijvingRepository);
                    }
                }

                if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.QRCode))
                {
                    inschrijvingVanRepo.QRCode = QRCode.GetNieuweQRCode();
                }
            }
            // === EINDE Controle voor manipulatie === //

            // === START null waarden wegwerken === //
            if (!inschrijvingVanRepo.AantalWagens.HasValue)
            {
                inschrijvingVanRepo.AantalWagens = 0;
            }
            if (!inschrijvingVanRepo.AantalAanhangwagens.HasValue)
            {
                inschrijvingVanRepo.AantalAanhangwagens = 0;
            }
            if (!inschrijvingVanRepo.AantalMobilhomes.HasValue)
            {
                inschrijvingVanRepo.AantalMobilhomes = 0;
            }
            // === EINDE null waarden wegwerken === //

            _inschrijvingRepository.UpdatenInschrijving(inschrijvingVanRepo);
            _inschrijvingRepository.Opslaan();

            //return NoContent();
            var inschrijvingTeRetourneren = _mapper.Map<InschrijvingVoorRaadpleegDto>(inschrijvingVanRepo);
            return CreatedAtRoute("GetInschrijving", new { inschrijvingsId = inschrijvingTeRetourneren.Id }, inschrijvingTeRetourneren);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch("{inschrijvingsId}", Name = "GedeeltelijkeUpdateInschrijving")]
        public IActionResult GedeeltelijkeUpdateInschrijving(Guid inschrijvingsId, [FromBody]JsonPatchDocument<InschrijvingVoorUpdateDto> patchDocument)
        {
            var inschrijvingVanRepo = _inschrijvingRepository.GetInschrijving(inschrijvingsId);
            if (inschrijvingVanRepo == null)
            {
                return NotFound($"Inschrijving '{inschrijvingsId}' niet gevonden.");
            }
            Guid inschrijvingsStatusVoorPatch = inschrijvingVanRepo.InschrijvingsstatusId.Value;

            var inschrijvingTePatchen = _mapper.Map<InschrijvingVoorUpdateDto>(inschrijvingVanRepo);
            patchDocument.ApplyTo(inschrijvingTePatchen, ModelState);

            if (!TryValidateModel(inschrijvingTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(inschrijvingTePatchen, inschrijvingVanRepo);

            // === START Controle voor manipulatie === // // === EINDE Controle voor manipulatie === //
            if (!_betaalmethodeRepository.BestaatBetaalmethode(inschrijvingVanRepo.BetaalmethodeId.Value))
            {
                return NotFound($"Betaalmethode '{inschrijvingVanRepo.BetaalmethodeId.Value}' niet gevonden.");
            }

            if (!_inschrijvingsstatusRepository.BestaatInschrijvingsstatus(inschrijvingVanRepo.InschrijvingsstatusId.Value))
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingVanRepo.InschrijvingsstatusId.Value}' niet gevonden.");
            }

            if (!_evenementRepository.BestaatEvenement(inschrijvingVanRepo.EvenementId.Value))
            {
                return NotFound($"Evenement '{inschrijvingVanRepo.EvenementId.Value}' niet gevonden.");
            }

            if (inschrijvingVanRepo.LidId.HasValue)
            {
                if (!_lidRepository.BestaatLid(inschrijvingVanRepo.LidId.Value))
                {
                    return NotFound($"Lid '{inschrijvingVanRepo.LidId.Value}' niet gevonden.");
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland().Id)
            {
                if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.Standnummer))
                {
                    return Conflict($"Bij het plannen van een inschrijving moet een standnummer worden opgegeven.");
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Afgekeurd().Id)
            {
                if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.RedenAfkeuring))
                {
                    return Conflict($"Bij het afkeuren van een inschrijving moet een reden tot afkeuring worden opgegeven.");
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Goedgekeurd().Id | inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland().Id)
            {
                if (inschrijvingVanRepo.BetaalmethodeId.Value == _betaalmethodeRepository.GetBetaalmethode_Overschrijving().Id)
                {
                    if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.GestructureerdeMededeling))
                    {
                        inschrijvingVanRepo.GestructureerdeMededeling = GestructureerdeMededeling.GetNieuweGestructureerdeMededeling(_inschrijvingRepository);
                    }
                }

                if (string.IsNullOrWhiteSpace(inschrijvingVanRepo.QRCode))
                {
                    inschrijvingVanRepo.QRCode = QRCode.GetNieuweQRCode();
                }
            }
            // === EINDE Controle voor manipulatie === //

            // === START null waarden wegwerken === //
            if (!inschrijvingVanRepo.AantalWagens.HasValue)
            {
                inschrijvingVanRepo.AantalWagens = 0;
            }
            if (!inschrijvingVanRepo.AantalAanhangwagens.HasValue)
            {
                inschrijvingVanRepo.AantalAanhangwagens = 0;
            }
            if (!inschrijvingVanRepo.AantalMobilhomes.HasValue)
            {
                inschrijvingVanRepo.AantalMobilhomes = 0;
            }
            // === EINDE null waarden wegwerken === //

            _inschrijvingRepository.UpdatenInschrijving(inschrijvingVanRepo);
            _inschrijvingRepository.Opslaan();

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Goedgekeurd().Id)
            {
                if (inschrijvingsStatusVoorPatch == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Aangevraagd().Id)
                {
                    _mailing.VerstuurMail_AanvraagGoedgekeurd(inschrijvingVanRepo);
                }
            }

            if (inschrijvingVanRepo.InschrijvingsstatusId.Value == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Afgekeurd().Id)
            {
                if (inschrijvingsStatusVoorPatch == _inschrijvingsstatusRepository.GetInschrijvingsstatus_Aangevraagd().Id)
                {
                    _mailing.VerstuurMail_AanvraagAfgekeurd(inschrijvingVanRepo);
                }
            }

            //return NoContent();
            var inschrijvingTeRetourneren = _mapper.Map<InschrijvingVoorRaadpleegDto>(inschrijvingVanRepo);
            return CreatedAtRoute("GetInschrijving", new { inschrijvingsId = inschrijvingTeRetourneren.Id }, inschrijvingTeRetourneren);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{inschrijvingsId}", Name = "VerwijderInschrijving")]
        public IActionResult VerwijderInschrijving(Guid inschrijvingsId)
        {
            var inschrijvingVanRepo = _inschrijvingRepository.GetInschrijving(inschrijvingsId);
            if (inschrijvingVanRepo == null)
            {
                return NotFound($"Inschrijving '{inschrijvingsId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            //if (_checkInsRepository.GetAantalCheckInsVanLid(inschrijvingsId) > 0)
            //{
            //    // return Conflict($"Er bestaan nog check-ins met als inschrijving '{inschrijvingsId}'");
            //    // Verwijderen van CheckIns?!
            //}
            // === EINDE Controle voor manipulatie === //

            _inschrijvingRepository.VerwijderenInschrijving(inschrijvingVanRepo);
            _inschrijvingRepository.Opslaan();

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpOptions(Name = "GetInschrijvingenOpties")]
        public IActionResult GetInschrijvingenOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpOptions("{inschrijvingsId}", Name = "GetInschrijvingOpties")]
        public IActionResult GetInschrijvingOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
