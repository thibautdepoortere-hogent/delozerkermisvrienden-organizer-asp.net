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
    [Route("api/evenementen")]
    public class EvenementenController : ControllerBase
    {
        private readonly IEvenementCategorieRepository _evenementCategorieRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly IMapper _mapper;

        public EvenementenController(IEvenementCategorieRepository evenementCategorieRepository, IEvenementRepository evenementRepository, IInschrijvingRepository inschrijvingRepository, IMapper mapper)
        {
            _evenementCategorieRepository = evenementCategorieRepository ?? throw new ArgumentNullException(nameof(evenementCategorieRepository));
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet(Name = "GetEvenementen")]
        public ActionResult<IEnumerable<EvenementVoorRaadpleegDto>> GetEvenementen([FromQuery] EvenementenResourceParameters evenementenResourceParameters)
        {
            if (evenementenResourceParameters != null)
            {
                if (evenementenResourceParameters.EvenementCategorie.HasValue)
                {
                    if (!_evenementCategorieRepository.BestaatEvenementCategorie(evenementenResourceParameters.EvenementCategorie.Value))
                    {
                        return NotFound($"Evenement categorie '{evenementenResourceParameters.EvenementCategorie.Value}' niet gevonden.");
                    }
                }

                if (evenementenResourceParameters.StartPeriode.HasValue && evenementenResourceParameters.EindPeriode.HasValue)
                {
                    if (evenementenResourceParameters.StartPeriode > evenementenResourceParameters.EindPeriode)
                    {
                        return BadRequest("De opgegeven periode is ongeldig.");
                    }
                }
            }

            var EvenementenVanRepo = _evenementRepository.GetEvenementen(evenementenResourceParameters);
            return Ok(_mapper.Map<IEnumerable<EvenementVoorRaadpleegDto>>(EvenementenVanRepo));
        }


        [HttpHead("{evenementId}")]
        [HttpGet("{evenementId}", Name = "GetEvenement")]
        public IActionResult GetEvenement(Guid evenementId)
        {
            var evenementVanRepo = _evenementRepository.GetEvenement(evenementId);
            if (evenementVanRepo == null)
            {
                return NotFound($"Evenement '{evenementId}' niet gevonden.");
            }
            return Ok(_mapper.Map<EvenementVoorRaadpleegDto>(evenementVanRepo));
        }


        [HttpPost(Name = "ToevoegenEvenement")]
        public ActionResult<EvenementVoorRaadpleegDto> ToevoegenEvenement([FromBody]EvenementVoorAanmaakDto evenement)
        {
            var evenementEntity = _mapper.Map<Entities.Evenement>(evenement);

            // === START Controle voor manipulatie === //
            if (_evenementRepository.BestaatEvenement(evenementEntity.Naam))
            {
                return Conflict("Er bestaat reeds een evenement met deze naam.");
            }

            if (evenementEntity.EvenementCategorieId.HasValue)
            {
                if (!_evenementCategorieRepository.BestaatEvenementCategorie(evenementEntity.EvenementCategorieId.Value))
                {
                    return NotFound($"Evenement categorie '{evenementEntity.EvenementCategorieId.Value}' niet gevonden.");
                }
            }
            // === EINDE Controle voor manipulatie === //

            _evenementRepository.ToevoegenEvenement(evenementEntity);
            _evenementRepository.Opslaan();

            var evenementTeRetourneren = _mapper.Map<EvenementVoorRaadpleegDto>(evenementEntity);
            return CreatedAtRoute("GetEvenement", new { evenementId = evenementTeRetourneren.Id }, evenementTeRetourneren);
        }


        [HttpPut("{evenementId}", Name = "VolledigeUpdateEvenement")]
        public IActionResult VolledigeUpdateEvenement(Guid evenementId, [FromBody]EvenementVoorUpdateDto evenement)
        {
            var evenementVanRepo = _evenementRepository.GetEvenement(evenementId);
            if (evenementVanRepo == null)
            {
                return NotFound($"Evenement '{evenementId}' niet gevonden.");
            }

            _mapper.Map(evenement, evenementVanRepo);

            // === START Controle voor manipulatie === //
            if (_evenementRepository.BestaatEvenementMetUitzonderingVan(evenementVanRepo.Naam, evenementId))
            {
                return Conflict($"Er bestaat reeds een evenement met deze naam.");
            }

            if (evenementVanRepo.EvenementCategorieId.HasValue)
            {
                if (!_evenementCategorieRepository.BestaatEvenementCategorie(evenementVanRepo.EvenementCategorieId.Value))
                {
                    return NotFound($"Evenement categorie '{evenementVanRepo.EvenementCategorieId.Value}' niet gevonden.");
                }
            }
            // === EINDE Controle voor manipulatie === //

            _evenementRepository.UpdatenEvenement(evenementVanRepo);
            _evenementRepository.Opslaan();

            //return NoContent();
            var evenementTeRetourneren = _mapper.Map<EvenementVoorRaadpleegDto>(evenementVanRepo);
            return CreatedAtRoute("GetEvenement", new { evenementId = evenementTeRetourneren.Id }, evenementTeRetourneren);
        }


        [HttpPatch("{evenementId}", Name = "GedeeltelijkeUpdateEvenement")]
        public IActionResult GedeeltelijkeUpdateEvenement(Guid evenementId, [FromBody]JsonPatchDocument<EvenementVoorUpdateDto> patchDocument)
        {
            var evenementVanRepo = _evenementRepository.GetEvenement(evenementId);
            if (evenementVanRepo == null)
            {
                return NotFound($"Evenement '{evenementId}' niet gevonden.");
            }

            var evenementTePatchen = _mapper.Map<EvenementVoorUpdateDto>(evenementVanRepo);
            patchDocument.ApplyTo(evenementTePatchen, ModelState);

            if (!TryValidateModel(evenementTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(evenementTePatchen, evenementVanRepo);

            // === START Controle voor manipulatie === //
            if (_evenementRepository.BestaatEvenementMetUitzonderingVan(evenementVanRepo.Naam, evenementId))
            {
                return Conflict($"Er bestaat reeds een evenement met deze naam.");
            }

            if (evenementVanRepo.EvenementCategorieId.HasValue)
            {
                if (!_evenementCategorieRepository.BestaatEvenementCategorie(evenementVanRepo.EvenementCategorieId.Value))
                {
                    return NotFound($"Evenement categorie '{evenementVanRepo.EvenementCategorieId.Value}' niet gevonden.");
                }
            }
            // === EINDE Controle voor manipulatie === //

            _evenementRepository.UpdatenEvenement(evenementVanRepo);
            _evenementRepository.Opslaan();

            //return NoContent();
            var evenementTeRetourneren = _mapper.Map<EvenementVoorRaadpleegDto>(evenementVanRepo);
            return CreatedAtRoute("GetEvenement", new { evenementId = evenementTeRetourneren.Id }, evenementTeRetourneren);
        }


        [HttpDelete("{evenementId}", Name = "VerwijderEvenement")]
        public IActionResult VerwijderEvenement(Guid evenementId)
        {
            var evenementVanRepo = _evenementRepository.GetEvenement(evenementId);
            if (evenementVanRepo == null)
            {
                return NotFound($"Evenement '{evenementId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            //if (_nieuwsbriefRepository.GetAantalNieuwsbrievenVanEvenement(evenementId) > 0)
            //{
            //    // return Conflict($"Er bestaan nog nieuwsbrieven met als evenement '{evenementId}'");
            //    // Verwijderen van Nieuwsbrieven?!
            //}
            // === EINDE Controle voor manipulatie === //

            _evenementRepository.VerwijderenEvenement(evenementVanRepo);
            _evenementRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetEvenementenOpties")]
        public IActionResult GetEvenementenOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{evenementId}", Name = "GetEvenementOpties")]
        public IActionResult GetEvenementOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
