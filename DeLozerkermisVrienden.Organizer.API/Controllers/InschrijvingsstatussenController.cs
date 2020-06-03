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
    [Route("api/inschrijvingsstatussen")]
    public class InschrijvingsstatussenController : ControllerBase
    {
        private readonly IInschrijvingsstatusRepository _inschrijvingsstatusRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly IMapper _mapper;

        public InschrijvingsstatussenController(IInschrijvingsstatusRepository inschrijvingsstatusRepository, IInschrijvingRepository inschrijvingRepository, IMapper mapper)
        {
            _inschrijvingsstatusRepository = inschrijvingsstatusRepository ?? throw new ArgumentNullException(nameof(inschrijvingsstatusRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [AllowAnonymous]
        [HttpGet(Name = "GetInschrijvingsstatussen")]
        public ActionResult<IEnumerable<InschrijvingsstatusVoorRaadpleegDto>> GetInschrijvingsstatussen()
        {
            var inschrijvingsstatussenVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatussen();
            return Ok(_mapper.Map<IEnumerable<InschrijvingsstatusVoorRaadpleegDto>>(inschrijvingsstatussenVanRepo));
        }


        [AllowAnonymous]
        [HttpHead("{inschrijvingsstatusId}")]
        [HttpGet("{inschrijvingsstatusId}", Name = "GetInschrijvingsstatus")]
        public IActionResult GetInschrijvingsstatus(Guid inschrijvingsstatusId)
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus(inschrijvingsstatusId);
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingsstatusId}' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo));
        }

        [HttpGet("aangevraagd", Name = "GetInschrijvingsstatusAanvraag")]
        public IActionResult GetInschrijvingsstatusAanvraag()
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus_Aangevraagd();
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus 'Aangevraagd' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo));
        }

        [HttpGet("goedgekeurd", Name = "GetInschrijvingsstatusGoedgekeurd")]
        public IActionResult GetInschrijvingsstatusGoedgekeurd()
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus_Goedgekeurd();
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus 'Goedgekeurd' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo));
        }

        [HttpGet("afgekeurd", Name = "GetInschrijvingsstatusAfgekeurd")]
        public IActionResult GetInschrijvingsstatusAfgekeurd()
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus_Afgekeurd();
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus 'Afgekeurd' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo));
        }

        [HttpGet("gepland", Name = "GetInschrijvingsstatusGepland")]
        public IActionResult GetInschrijvingsstatusGepland()
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus_Gepland();
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus 'Gepland' niet gevonden.");
            }
            return Ok(_mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo));
        }


        [HttpPost(Name = "ToevoegenInschrijvingsstatus")]
        public ActionResult<InschrijvingsstatusVoorRaadpleegDto> ToevoegenInschrijvingsstatus([FromBody]InschrijvingsstatusVoorAanmaakDto inschrijvingsstatus)
        {
            var inschrijvingsstatusEntity = _mapper.Map<Entities.Inschrijvingsstatus>(inschrijvingsstatus);

            // === START Controle voor manipulatie === //
            if (_inschrijvingsstatusRepository.BestaatInschrijvingsstatus(inschrijvingsstatusEntity.Naam))
            {
                return Conflict($"Er bestaat reeds een inschrijvingsstatus met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _inschrijvingsstatusRepository.ToevoegenInschrijvingsstatus(inschrijvingsstatusEntity);
            _inschrijvingsstatusRepository.Opslaan();

            var inschrijvingsstatusTeRetourneren = _mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusEntity);
            return CreatedAtRoute("GetInschrijvingsstatus", new { inschrijvingsstatusId = inschrijvingsstatusTeRetourneren.Id }, inschrijvingsstatusTeRetourneren);
        }


        [HttpPut("{inschrijvingsstatusId}", Name = "VolledigeUpdateInschrijvingsstatus")]
        public IActionResult VolledigeUpdateInschrijvingsstatus(Guid inschrijvingsstatusId, [FromBody]InschrijvingsstatusVoorUpdateDto inschrijvingsstatus)
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus(inschrijvingsstatusId);
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingsstatusId}' niet gevonden.");
            }

            _mapper.Map(inschrijvingsstatus, inschrijvingsstatusVanRepo);

            // === START Controle voor manipulatie === //
            if (_inschrijvingsstatusRepository.BestaatInschrijvingsstatusMetUitzonderingVan(inschrijvingsstatusVanRepo.Naam, inschrijvingsstatusId))
            {
                return Conflict($"Er bestaat reeds een inschrijvingsstatus met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _inschrijvingsstatusRepository.UpdatenInschrijvingsstatus(inschrijvingsstatusVanRepo);
            _inschrijvingsstatusRepository.Opslaan();

            //return NoContent();
            var inschrijvingsstatusTeRetourneren = _mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo);
            return CreatedAtRoute("GetInschrijvingsstatus", new { inschrijvingsstatusId = inschrijvingsstatusTeRetourneren.Id }, inschrijvingsstatusTeRetourneren);
        }


        [HttpPatch("{inschrijvingsstatusId}", Name = "GedeeltelijkeUpdateInschrijvingsstatus")]
        public IActionResult GedeeltelijkeUpdateInschrijvingsstatus(Guid inschrijvingsstatusId, [FromBody]JsonPatchDocument<InschrijvingsstatusVoorUpdateDto> patchDocument)
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus(inschrijvingsstatusId);
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingsstatusId}' niet gevonden.");
            }

            var inschrijvingsstatusTePatchen = _mapper.Map<InschrijvingsstatusVoorUpdateDto>(inschrijvingsstatusVanRepo);
            patchDocument.ApplyTo(inschrijvingsstatusTePatchen, ModelState);

            if (!TryValidateModel(inschrijvingsstatusTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(inschrijvingsstatusTePatchen, inschrijvingsstatusVanRepo);

            // === START Controle voor manipulatie === //
            if (_inschrijvingsstatusRepository.BestaatInschrijvingsstatusMetUitzonderingVan(inschrijvingsstatusVanRepo.Naam, inschrijvingsstatusId))
            {
                return Conflict($"Er bestaat reeds een inschrijvingsstatus met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _inschrijvingsstatusRepository.UpdatenInschrijvingsstatus(inschrijvingsstatusVanRepo);
            _inschrijvingsstatusRepository.Opslaan();

            //return NoContent();
            var inschrijvingsstatusTeRetourneren = _mapper.Map<InschrijvingsstatusVoorRaadpleegDto>(inschrijvingsstatusVanRepo);
            return CreatedAtRoute("GetInschrijvingsstatus", new { inschrijvingsstatusId = inschrijvingsstatusTeRetourneren.Id }, inschrijvingsstatusTeRetourneren);
        }


        [HttpDelete("{inschrijvingsstatusId}", Name = "VerwijderInschrijvingsstatus")]
        public IActionResult VerwijderInschrijvingsstatus(Guid inschrijvingsstatusId)
        {
            var inschrijvingsstatusVanRepo = _inschrijvingsstatusRepository.GetInschrijvingsstatus(inschrijvingsstatusId);
            if (inschrijvingsstatusVanRepo == null)
            {
                return NotFound($"Inschrijvingsstatus '{inschrijvingsstatusId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            if (_inschrijvingRepository.GetAantalInschrijvingenVanInschrijvingsstatus(inschrijvingsstatusId) > 0)
            {
                return Conflict($"Er bestaan nog inschrijvingen met als inschrijvingsstatus '{inschrijvingsstatusId}'");
            }
            // === EINDE Controle voor manipulatie === //

            _inschrijvingsstatusRepository.VerwijderenInschrijvingsstatus(inschrijvingsstatusVanRepo);
            _inschrijvingsstatusRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetInschrijvingsstatussenOpties")]
        public IActionResult GetInschrijvingsstatussenOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{inschrijvingsstatusId}", Name = "GetInschrijvingsstatusOpties")]
        public IActionResult GetInschrijvingsstatusOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
