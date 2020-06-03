using AutoMapper;
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
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/betaaltransacties")]
    public class BetaaltransactiesController : ControllerBase
    {
        private readonly IBetaalmethodeRepository _betaalmethodeRepository;
        private readonly ILidRepository _lidRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly IBetaaltransactieRepository _betaaltransactieRepository;
        private readonly IMapper _mapper;

        public BetaaltransactiesController(IBetaalmethodeRepository betaalmethodeRepository, ILidRepository lidRepository, IInschrijvingRepository inschrijvingRepository, IBetaaltransactieRepository betaaltransactieRepository, IMapper mapper)
        {
            _betaalmethodeRepository = betaalmethodeRepository ?? throw new ArgumentNullException(nameof(_betaalmethodeRepository));
            _lidRepository = lidRepository ?? throw new ArgumentNullException(nameof(_lidRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _betaaltransactieRepository = betaaltransactieRepository ?? throw new ArgumentNullException(nameof(betaaltransactieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetBetaaltransacties")]
        public ActionResult<IEnumerable<BetaaltransactieVoorRaadpleegDto>> GetBetaaltransacties([FromQuery] BetaaltransactiesResourceParameters betaaltransactiesResourceParameters)
        {
            if (betaaltransactiesResourceParameters != null)
            {
                if (betaaltransactiesResourceParameters.Inschrijving.HasValue)
                {
                    if (!_inschrijvingRepository.BestaatInschrijving(betaaltransactiesResourceParameters.Inschrijving.Value))
                    {
                        return NotFound($"Inschrijving '{betaaltransactiesResourceParameters.Inschrijving.Value}' niet gevonden.");
                    }
                }

                if (betaaltransactiesResourceParameters.Betaalmethode.HasValue)
                {
                    if (!_betaalmethodeRepository.BestaatBetaalmethode(betaaltransactiesResourceParameters.Betaalmethode.Value))
                    {
                        return NotFound($"Betaalmethode '{betaaltransactiesResourceParameters.Betaalmethode.Value}' niet gevonden.");
                    }
                }

                if (betaaltransactiesResourceParameters.Lid.HasValue)
                {
                    if (!_lidRepository.BestaatLid(betaaltransactiesResourceParameters.Lid.Value))
                    {
                        return NotFound($"Lid '{betaaltransactiesResourceParameters.Lid.Value}' niet gevonden.");
                    }
                }
            }

            var BetaaltransactiesVanRepo = _betaaltransactieRepository.GetBetaaltransacties(betaaltransactiesResourceParameters);
            return Ok(_mapper.Map<IEnumerable<BetaaltransactieVoorRaadpleegDto>>(BetaaltransactiesVanRepo));
        }


        [HttpHead("{betaaltransactieId}")]
        [HttpGet("{betaaltransactieId}", Name = "GetBetaaltransactie")]
        public IActionResult GetBetaaltransactie(Guid betaaltransactieId)
        {
            var betaaltransactieVanRepo = _betaaltransactieRepository.GetBetaaltransactie(betaaltransactieId);
            if (betaaltransactieVanRepo == null)
            {
                return NotFound($"Betaaltransactie '{betaaltransactieId}' niet gevonden.");
            }
            return Ok(_mapper.Map<BetaaltransactieVoorRaadpleegDto>(betaaltransactieVanRepo));
        }


        [HttpPost(Name = "ToevoegenBetaaltransactie")]
        public ActionResult<BetaaltransactieVoorRaadpleegDto> ToevoegenBetaaltransactie([FromBody]BetaaltransactieVoorAanmaakDto betaaltransactie)
        {
            var betaaltransactieEntity = _mapper.Map<Entities.Betaaltransactie>(betaaltransactie);

            // === START Controle voor manipulatie === //
            if (betaaltransactieEntity.InschrijvingsId.HasValue)
            {
                if (!_inschrijvingRepository.BestaatInschrijving(betaaltransactieEntity.InschrijvingsId.Value))
                {
                    return NotFound($"Inschrijving '{betaaltransactieEntity.InschrijvingsId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieEntity.BetaalmethodeId.HasValue)
            {
                if (!_betaalmethodeRepository.BestaatBetaalmethode(betaaltransactieEntity.BetaalmethodeId.Value))
                {
                    return NotFound($"Betaalmethode '{betaaltransactieEntity.BetaalmethodeId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieEntity.LidId.HasValue)
            {
                if (!_lidRepository.BestaatLid(betaaltransactieEntity.LidId.Value))
                {
                    return NotFound($"Lid '{betaaltransactieEntity.LidId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieEntity.Bedrag == 0m)
            {
                return BadRequest("Bedrag kan niet 0 zijn.");
            }
            // === EINDE Controle voor manipulatie === //

            _betaaltransactieRepository.ToevoegenBetaaltransactie(betaaltransactieEntity);
            _betaaltransactieRepository.Opslaan();

            var betaaltransactieTeRetourneren = _mapper.Map<BetaaltransactieVoorRaadpleegDto>(betaaltransactieEntity);
            return CreatedAtRoute("GetBetaaltransactie", new { betaaltransactieId = betaaltransactieTeRetourneren.Id }, betaaltransactieTeRetourneren);
        }


        [HttpPut("{betaaltransactieId}", Name = "VolledigeUpdateBetaaltransactie")]
        public IActionResult VolledigeUpdateBetaaltransactie(Guid betaaltransactieId, [FromBody]BetaaltransactieVoorUpdateDto betaaltransactie)
        {
            var betaaltransactieVanRepo = _betaaltransactieRepository.GetBetaaltransactie(betaaltransactieId);
            if (betaaltransactieVanRepo == null)
            {
                return NotFound($"Betaaltransactie '{betaaltransactieId}' niet gevonden.");
            }

            _mapper.Map(betaaltransactie, betaaltransactieVanRepo);

            // === START Controle voor manipulatie === //
            if (betaaltransactieVanRepo.InschrijvingsId.HasValue)
            {
                if (!_inschrijvingRepository.BestaatInschrijving(betaaltransactieVanRepo.InschrijvingsId.Value))
                {
                    return NotFound($"Inschrijving '{betaaltransactieVanRepo.InschrijvingsId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieVanRepo.BetaalmethodeId.HasValue)
            {
                if (!_betaalmethodeRepository.BestaatBetaalmethode(betaaltransactieVanRepo.BetaalmethodeId.Value))
                {
                    return NotFound($"Betaalmethode '{betaaltransactieVanRepo.BetaalmethodeId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieVanRepo.LidId.HasValue)
            {
                if (!_lidRepository.BestaatLid(betaaltransactieVanRepo.LidId.Value))
                {
                    return NotFound($"Lid '{betaaltransactieVanRepo.LidId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieVanRepo.Bedrag == 0m)
            {
                return BadRequest("Bedrag kan niet 0 zijn.");
            }
            // === EINDE Controle voor manipulatie === //

            _betaaltransactieRepository.UpdatenBetaaltransactie(betaaltransactieVanRepo);
            _betaaltransactieRepository.Opslaan();

            //return NoContent();
            var betaaltransactieTeRetourneren = _mapper.Map<BetaaltransactieVoorRaadpleegDto>(betaaltransactieVanRepo);
            return CreatedAtRoute("GetBetaaltransactie", new { betaaltransactieId = betaaltransactieTeRetourneren.Id }, betaaltransactieTeRetourneren);
        }


        [HttpPatch("{betaaltransactieId}", Name = "GedeeltelijkeUpdateBetaaltransactie")]
        public IActionResult GedeeltelijkeUpdateBetaaltransactie(Guid betaaltransactieId, [FromBody]JsonPatchDocument<BetaaltransactieVoorUpdateDto> patchDocument)
        {
            var betaaltransactieVanRepo = _betaaltransactieRepository.GetBetaaltransactie(betaaltransactieId);
            if (betaaltransactieVanRepo == null)
            {
                return NotFound($"Betaaltransactie '{betaaltransactieId}' niet gevonden.");
            }

            var betaaltransactieTePatchen = _mapper.Map<BetaaltransactieVoorUpdateDto>(betaaltransactieVanRepo);
            patchDocument.ApplyTo(betaaltransactieTePatchen, ModelState);

            if (!TryValidateModel(betaaltransactieTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(betaaltransactieTePatchen, betaaltransactieVanRepo);

            // === START Controle voor manipulatie === //
            if (betaaltransactieVanRepo.InschrijvingsId.HasValue)
            {
                if (!_inschrijvingRepository.BestaatInschrijving(betaaltransactieVanRepo.InschrijvingsId.Value))
                {
                    return NotFound($"Inschrijving '{betaaltransactieVanRepo.InschrijvingsId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieVanRepo.BetaalmethodeId.HasValue)
            {
                if (!_betaalmethodeRepository.BestaatBetaalmethode(betaaltransactieVanRepo.BetaalmethodeId.Value))
                {
                    return NotFound($"Betaalmethode '{betaaltransactieVanRepo.BetaalmethodeId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieVanRepo.LidId.HasValue)
            {
                if (!_lidRepository.BestaatLid(betaaltransactieVanRepo.LidId.Value))
                {
                    return NotFound($"Lid '{betaaltransactieVanRepo.LidId.Value}' niet gevonden.");
                }
            }

            if (betaaltransactieVanRepo.Bedrag == 0m)
            {
                return BadRequest("Bedrag kan niet 0 zijn.");
            }
            // === EINDE Controle voor manipulatie === //

            _betaaltransactieRepository.UpdatenBetaaltransactie(betaaltransactieVanRepo);
            _betaaltransactieRepository.Opslaan();

            //return NoContent();
            var betaaltransactieTeRetourneren = _mapper.Map<BetaaltransactieVoorRaadpleegDto>(betaaltransactieVanRepo);
            return CreatedAtRoute("GetBetaaltransactie", new { betaaltransactieId = betaaltransactieTeRetourneren.Id }, betaaltransactieTeRetourneren);
        }


        [HttpDelete("{betaaltransactieId}", Name = "VerwijderBetaaltransactie")]
        public IActionResult VerwijderBetaaltransactie(Guid betaaltransactieId)
        {
            var betaaltransactieVanRepo = _betaaltransactieRepository.GetBetaaltransactie(betaaltransactieId);
            if (betaaltransactieVanRepo == null)
            {
                return NotFound($"Betaaltransactie '{betaaltransactieId}' niet gevonden.");
            }

            _betaaltransactieRepository.VerwijderenBetaaltransactie(betaaltransactieVanRepo);
            _betaaltransactieRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetBetaaltransactiesOpties")]
        public IActionResult GetBetaaltransactiesOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{betaaltransactieId}", Name = "GetBetaaltransactieOpties")]
        public IActionResult GetBetaaltransactieOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
