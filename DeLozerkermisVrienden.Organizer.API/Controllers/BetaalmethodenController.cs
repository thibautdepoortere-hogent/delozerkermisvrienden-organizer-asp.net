using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Models;
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
    [Route("api/betaalmethoden")]
    public class BetaalmethodenController : ControllerBase
    {
        private readonly IBetaalmethodeRepository _betaalmethodeRepository;
        private readonly IBetaaltransactieRepository _betaaltransactieRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly IMapper _mapper;

        public BetaalmethodenController(IBetaalmethodeRepository betaalmethodeRepository, IBetaaltransactieRepository betaaltransactieRepository, IInschrijvingRepository inschrijvingRepository, IMapper mapper)
        {
            _betaalmethodeRepository = betaalmethodeRepository ?? throw new ArgumentNullException(nameof(betaalmethodeRepository));
            _betaaltransactieRepository = betaaltransactieRepository ?? throw new ArgumentNullException(nameof(betaaltransactieRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [AllowAnonymous]
        [HttpGet(Name = "GetBetaalmethoden")]
        public ActionResult<IEnumerable<BetaalmethodeVoorRaadpleegDto>> GetBetaalmethoden()
        {
            var betaalmethodenVanRepo = _betaalmethodeRepository.GetBetaalmethoden();
            return Ok(_mapper.Map<IEnumerable<BetaalmethodeVoorRaadpleegDto>>(betaalmethodenVanRepo));
        }


        [AllowAnonymous]
        [HttpHead("{betaalmethodeId}")]
        [HttpGet("{betaalmethodeId}", Name = "GetBetaalmethode")]
        public IActionResult GetBetaalmethode(Guid betaalmethodeId)
        {
            var betaalmethodeVanRepo = _betaalmethodeRepository.GetBetaalmethode(betaalmethodeId);
            if (betaalmethodeVanRepo == null)
            {
                return NotFound($"Betaalmethode '{betaalmethodeId}' niet gevonden.");
            }
            return Ok(_mapper.Map<BetaalmethodeVoorRaadpleegDto>(betaalmethodeVanRepo));
        }

        [HttpGet("overschrijving", Name = "GetOverschrijvingsBetaalmethode")]
        public IActionResult GetOverschrijvingsBetaalmethode()
        {
            var betaalmethodeVanRepo = _betaalmethodeRepository.GetBetaalmethode_Overschrijving();
            if (betaalmethodeVanRepo == null)
            {
                return NotFound($"Er is geen betaalmethode voor overschrijving aanwezig.");
            }
            return Ok(_mapper.Map<BetaalmethodeVoorRaadpleegDto>(betaalmethodeVanRepo));
        }


        [HttpPost(Name = "ToevoegenBetaalmethode")]
        public ActionResult<BetaalmethodeVoorRaadpleegDto> ToevoegenBetaalmethode([FromBody]BetaalmethodeVoorAanmaakDto betaalmethode)
        {
            var betaalmethodeEntity = _mapper.Map<Entities.Betaalmethode>(betaalmethode);

            // === START Controle voor manipulatie === //
            if (_betaalmethodeRepository.BestaatBetaalmethode(betaalmethodeEntity.Naam))
            {
                return Conflict($"Er bestaat reeds een betaalmethode met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _betaalmethodeRepository.ToevoegenBetaalmethode(betaalmethodeEntity);
            _betaalmethodeRepository.Opslaan();

            var betaalmethodeTeRetourneren = _mapper.Map<BetaalmethodeVoorRaadpleegDto>(betaalmethodeEntity);
            return CreatedAtRoute("GetBetaalmethode", new { betaalmethodeId = betaalmethodeTeRetourneren.Id }, betaalmethodeTeRetourneren);
        }


        [HttpPut("{betaalmethodeId}", Name = "VolledigeUpdateBetaalmethode")]
        public IActionResult VolledigeUpdateBetaalmethode(Guid betaalmethodeId, [FromBody]BetaalmethodeVoorUpdateDto betaalmethode)
        {
            var betaalmethodeVanRepo = _betaalmethodeRepository.GetBetaalmethode(betaalmethodeId);
            if (betaalmethodeVanRepo == null)
            {
                return NotFound($"Betaalmethode '{betaalmethodeId}' niet gevonden.");
            }

            _mapper.Map(betaalmethode, betaalmethodeVanRepo);

            // === START Controle voor manipulatie === //
            if (_betaalmethodeRepository.BestaatBetaalmethodeMetUitzonderingVan(betaalmethodeVanRepo.Naam, betaalmethodeId))
            {
                return Conflict($"Er bestaat reeds een betaalmethode met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _betaalmethodeRepository.UpdatenBetaalmethode(betaalmethodeVanRepo);
            _betaalmethodeRepository.Opslaan();

            //return NoContent();
            var betaalmethodeTeRetourneren = _mapper.Map<BetaalmethodeVoorRaadpleegDto>(betaalmethodeVanRepo);
            return CreatedAtRoute("GetBetaalmethode", new { betaalmethodeId = betaalmethodeTeRetourneren.Id }, betaalmethodeTeRetourneren);
        }


        [HttpPatch("{betaalmethodeId}", Name = "GedeeltelijkeUpdateBetaalmethode")]
        public IActionResult GedeeltelijkeUpdateBetaalmethode(Guid betaalmethodeId, [FromBody]JsonPatchDocument<BetaalmethodeVoorUpdateDto> patchDocument)
        {
            var betaalmethodeVanRepo = _betaalmethodeRepository.GetBetaalmethode(betaalmethodeId);
            if (betaalmethodeVanRepo == null)
            {
                return NotFound($"Betaalmethode '{betaalmethodeId}' niet gevonden.");
            }

            var betaalmethodeTePatchen = _mapper.Map<BetaalmethodeVoorUpdateDto>(betaalmethodeVanRepo);
            patchDocument.ApplyTo(betaalmethodeTePatchen, ModelState);

            if (!TryValidateModel(betaalmethodeTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(betaalmethodeTePatchen, betaalmethodeVanRepo);

            // === START Controle voor manipulatie === //
            if (_betaalmethodeRepository.BestaatBetaalmethodeMetUitzonderingVan(betaalmethodeVanRepo.Naam, betaalmethodeId))
            {
                return Conflict($"Er bestaat reeds een betaalmethode met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _betaalmethodeRepository.UpdatenBetaalmethode(betaalmethodeVanRepo);
            _betaalmethodeRepository.Opslaan();

            //return NoContent();
            var betaalmethodeTeRetourneren = _mapper.Map<BetaalmethodeVoorRaadpleegDto>(betaalmethodeVanRepo);
            return CreatedAtRoute("GetBetaalmethode", new { betaalmethodeId = betaalmethodeTeRetourneren.Id }, betaalmethodeTeRetourneren);
        }


        [HttpDelete("{betaalmethodeId}", Name = "VerwijderBetaalmethode")]
        public IActionResult VerwijderBetaalmethode(Guid betaalmethodeId)
        {
            var betaalmethodeVanRepo = _betaalmethodeRepository.GetBetaalmethode(betaalmethodeId);
            if (betaalmethodeVanRepo == null)
            {
                return NotFound($"Betaalmethode '{betaalmethodeId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            if (_inschrijvingRepository.GetAantalInschrijvingenVanBetaalmethode(betaalmethodeId) > 0)
            {
                return Conflict($"Er bestaan nog inschrijvingen met als betaalmethode '{betaalmethodeId}'");
            }

            if (_betaaltransactieRepository.GetAantalBetaaltransactiesVanBetaalmethode(betaalmethodeId) > 0)
            {
                return Conflict($"Er bestaan nog betaaltransacties met als betaalmethode '{betaalmethodeId}'");
            }
            // === EINDE Controle voor manipulatie === //

            _betaalmethodeRepository.VerwijderenBetaalmethode(betaalmethodeVanRepo);
            _betaalmethodeRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetBetaalmethodenOpties")]
        public IActionResult GetBetaalmethodenOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{betaalmethodeId}", Name = "GetBetaalmethodeOpties")]
        public IActionResult GetBetaalmethodeOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
