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
    [Route("api/leden")]
    public class LedenController : ControllerBase
    {
        private readonly ILidRepository _lidRepository;
        private readonly IBetaaltransactieRepository _betaaltransactieRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly ICheckInRepository _checkInRepository;
        private readonly IMapper _mapper;

        public LedenController(ILidRepository lidRepository, IBetaaltransactieRepository betaaltransactieRepository, IInschrijvingRepository inschrijvingRepository, ICheckInRepository checkInRepository, IMapper mapper)
        {
            _lidRepository = lidRepository ?? throw new ArgumentNullException(nameof(lidRepository));
            _betaaltransactieRepository = betaaltransactieRepository ?? throw new ArgumentNullException(nameof(betaaltransactieRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(checkInRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet(Name = "GetLeden")]
        public ActionResult<IEnumerable<LidVoorRaadpleegDto>> GetLeden([FromQuery] LedenResourceParameters ledenResourceParameters)
        {
            var ledenVanRepo = _lidRepository.GetLeden(ledenResourceParameters);
            return Ok(_mapper.Map<IEnumerable<LidVoorRaadpleegDto>>(ledenVanRepo));
        }


        [HttpHead("{lidId}")]
        [HttpGet("{lidId}", Name = "GetLid")]
        public IActionResult GetLid(Guid lidId)
        {
            var lidVanRepo = _lidRepository.GetLid(lidId);
            if (lidVanRepo == null)
            {
                return NotFound($"Lid '{lidId}' niet gevonden.");
            }
            return Ok(_mapper.Map<LidVoorRaadpleegDto>(lidVanRepo));
        }


        [HttpPost(Name = "ToevoegenLid")]
        public ActionResult<LidVoorRaadpleegDto> ToevoegenLid([FromBody]LidVoorAanmaakDto lid)
        {
            var lidEntity = _mapper.Map<Entities.Lid>(lid);

            // === START Controle voor manipulatie === //
            if (_lidRepository.BestaatLid(lidEntity.Email))
            {
                return Conflict($"Er bestaat reeds een lid met dit e-mailadres.");
            }
            // === EINDE Controle voor manipulatie === //

            _lidRepository.ToevoegenLid(lidEntity);
            _lidRepository.Opslaan();

            var lidTeRetourneren = _mapper.Map<LidVoorRaadpleegDto>(lidEntity);
            return CreatedAtRoute("GetLid", new { lidId = lidTeRetourneren.Id }, lidTeRetourneren);
        }


        [HttpPut("{lidId}", Name = "VolledigeUpdateLid")]
        public IActionResult VolledigeUpdateLid(Guid lidId, [FromBody]LidVoorUpdateDto lid)
        {
            var lidVanRepo = _lidRepository.GetLid(lidId);
            if (lidVanRepo == null)
            {
                return NotFound($"Lid '{lidId}' niet gevonden.");
            }

            _mapper.Map(lid, lidVanRepo);

            // === START Controle voor manipulatie === //
            if (_lidRepository.BestaatLidMetUitzonderingVan(lidVanRepo.Email, lidId))
            {
                return Conflict($"Er bestaat reeds een lid met dit e-mailadres.");
            }
            // === EINDE Controle voor manipulatie === //

            _lidRepository.UpdatenLid(lidVanRepo);
            _lidRepository.Opslaan();

            //return NoContent();
            var lidTeRetourneren = _mapper.Map<LidVoorRaadpleegDto>(lidVanRepo);
            return CreatedAtRoute("GetLid", new { lidId = lidTeRetourneren.Id }, lidTeRetourneren);
        }


        [HttpPatch("{lidId}", Name = "GedeeltelijkeUpdateLid")]
        public IActionResult GedeeltelijkeUpdateLid(Guid lidId, [FromBody]JsonPatchDocument<LidVoorUpdateDto> patchDocument)
        {
            var lidVanRepo = _lidRepository.GetLid(lidId);
            if (lidVanRepo == null)
            {
                return NotFound($"Lid '{lidId}' niet gevonden.");
            }

            var lidTePatchen = _mapper.Map<LidVoorUpdateDto>(lidVanRepo);
            patchDocument.ApplyTo(lidTePatchen, ModelState);

            if (!TryValidateModel(lidTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(lidTePatchen, lidVanRepo);

            // === START Controle voor manipulatie === //
            if (_lidRepository.BestaatLidMetUitzonderingVan(lidVanRepo.Email, lidId))
            {
                return Conflict($"Er bestaat reeds een lid met dit e-mailadres.");
            }
            // === EINDE Controle voor manipulatie === //

            _lidRepository.UpdatenLid(lidVanRepo);
            _lidRepository.Opslaan();

            //return NoContent();
            var lidTeRetourneren = _mapper.Map<LidVoorRaadpleegDto>(lidVanRepo);
            return CreatedAtRoute("GetLid", new { lidId = lidTeRetourneren.Id }, lidTeRetourneren);
        }


        [HttpDelete("{lidId}", Name = "VerwijderLid")]
        public IActionResult VerwijderLid(Guid lidId)
        {
            var lidVanRepo = _lidRepository.GetLid(lidId);
            if (lidVanRepo == null)
            {
                return NotFound($"Lid '{lidId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            if (_inschrijvingRepository.GetAantalInschrijvingenVanLid(lidId) > 0)
            {
                return Conflict($"Er bestaan nog inschrijvingen met als lid '{lidId}'");
            }

            if (_betaaltransactieRepository.GetAantalBetaaltransactiesVanLid(lidId) > 0)
            {
                return Conflict($"Er bestaan nog betaaltransacties met als lid '{lidId}'");
            }

            if (_checkInRepository.GetAantalCheckInsVanLid(lidId) > 0)
            {
                return Conflict($"Er bestaan nog check-ins met als lid '{lidId}'");
            }

            //if (_checkInsRepository.GetAantalCheckInsVanLid(lidId) > 0)
            //{
            //    return Conflict($"Er bestaan nog check-ins met als lid '{lidId}'");
            //}

            //if (_loginRepository.GetAantalLoginsVanLid(lidId) > 0)
            //{
            //    // return Conflict($"Er bestaan nog logins met als lid '{lidId}'");
            //    // Verwijderen van Logins?!
            //}
            // === EINDE Controle voor manipulatie === //

            _lidRepository.VerwijderenLid(lidVanRepo);
            _lidRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetLedenOpties")]
        public IActionResult GetLedenOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{lidId}", Name = "GetLidOpties")]
        public IActionResult GetLidOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
