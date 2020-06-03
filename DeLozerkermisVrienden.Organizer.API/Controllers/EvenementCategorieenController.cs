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
    [Route("api/evenementCategorieen")]
    public class EvenementCategorieenController : ControllerBase
    {
        private readonly IEvenementCategorieRepository _evenementCategorieRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly IMapper _mapper;

        public EvenementCategorieenController(IEvenementCategorieRepository evenementCategorieRepository, IEvenementRepository evenementRepository, IMapper mapper)
        {
            _evenementCategorieRepository = evenementCategorieRepository ?? throw new ArgumentNullException(nameof(evenementCategorieRepository));
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet(Name = "GetEvenementCategorieen")]
        public ActionResult<IEnumerable<EvenementCategorieVoorRaadpleegDto>> GetEvenementCategorieen()
        {
            var EvenementCategorieenVanRepo = _evenementCategorieRepository.GetEvenementCategorieen();
            return Ok(_mapper.Map<IEnumerable<EvenementCategorieVoorRaadpleegDto>>(EvenementCategorieenVanRepo));
        }


        [HttpHead("{evenementCategorieId}")]
        [HttpGet("{evenementCategorieId}", Name = "GetEvenementCategorie")]
        public IActionResult GetEvenementCategorie(Guid evenementCategorieId)
        {
            var evenementCategorieVanRepo = _evenementCategorieRepository.GetEvenementCategorie(evenementCategorieId);
            if (evenementCategorieVanRepo == null)
            {
                return NotFound($"Evenement categorie '{evenementCategorieId}' niet gevonden.");
            }
            return Ok(_mapper.Map<EvenementCategorieVoorRaadpleegDto>(evenementCategorieVanRepo));
        }


        [HttpPost(Name = "ToevoegenEvenementCategorie")]
        public ActionResult<EvenementCategorieVoorRaadpleegDto> ToevoegenEvenementCategorie([FromBody]EvenementCategorieVoorAanmaakDto evenementCategorie)
        {
            var evenementCategorieEntity = _mapper.Map<Entities.EvenementCategorie>(evenementCategorie);

            // === START Controle voor manipulatie === //
            if (_evenementCategorieRepository.BestaatEvenementCategorie(evenementCategorieEntity.Naam))
            {
                return Conflict($"Er bestaat reeds een evenement categorie met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _evenementCategorieRepository.ToevoegenEvenementCategorie(evenementCategorieEntity);
            _evenementCategorieRepository.Opslaan();

            var evenementCategorieTeRetourneren = _mapper.Map<EvenementCategorieVoorRaadpleegDto>(evenementCategorieEntity);
            return CreatedAtRoute("GetEvenementCategorie", new { evenementCategorieId = evenementCategorieTeRetourneren.Id }, evenementCategorieTeRetourneren);
        }


        [HttpPut("{evenementCategorieId}", Name = "VolledigeUpdateEvenementCategorie")]
        public IActionResult VolledigeUpdateEvenementCategorie(Guid evenementCategorieId, [FromBody]EvenementCategorieVoorUpdateDto evenementCategorie)
        {
            var evenementCategorieVanRepo = _evenementCategorieRepository.GetEvenementCategorie(evenementCategorieId);
            if (evenementCategorieVanRepo == null)
            {
                return NotFound($"Evenement categorie '{evenementCategorieId}' niet gevonden.");
            }

            _mapper.Map(evenementCategorie, evenementCategorieVanRepo);

            // === START Controle voor manipulatie === //
            if (_evenementCategorieRepository.BestaatEvenementCategorieMetUitzonderingVan(evenementCategorieVanRepo.Naam, evenementCategorieId))
            {
                return Conflict($"Er bestaat reeds een evenement categorie met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _evenementCategorieRepository.UpdatenEvenementCategorie(evenementCategorieVanRepo);
            _evenementCategorieRepository.Opslaan();

            //return NoContent();
            var evenementCategorieTeRetourneren = _mapper.Map<EvenementCategorieVoorRaadpleegDto>(evenementCategorieVanRepo);
            return CreatedAtRoute("GetEvenementCategorie", new { evenementCategorieId = evenementCategorieTeRetourneren.Id }, evenementCategorieTeRetourneren);
        }


        [HttpPatch("{evenementCategorieId}", Name = "GedeeltelijkeUpdateEvenementCategorie")]
        public IActionResult GedeeltelijkeUpdateEvenementCategorie(Guid evenementCategorieId, [FromBody]JsonPatchDocument<EvenementCategorieVoorUpdateDto> patchDocument)
        {
            var evenementCategorieVanRepo = _evenementCategorieRepository.GetEvenementCategorie(evenementCategorieId);
            if (evenementCategorieVanRepo == null)
            {
                return NotFound($"Evenement categorie '{evenementCategorieId}' niet gevonden.");
            }

            var evenementCategorieTePatchen = _mapper.Map<EvenementCategorieVoorUpdateDto>(evenementCategorieVanRepo);
            patchDocument.ApplyTo(evenementCategorieTePatchen, ModelState);

            if (!TryValidateModel(evenementCategorieTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(evenementCategorieTePatchen, evenementCategorieVanRepo);

            // === START Controle voor manipulatie === //
            if (_evenementCategorieRepository.BestaatEvenementCategorieMetUitzonderingVan(evenementCategorieVanRepo.Naam, evenementCategorieId))
            {
                return Conflict($"Er bestaat reeds een evenement categorie met deze naam.");
            }
            // === EINDE Controle voor manipulatie === //

            _evenementCategorieRepository.UpdatenEvenementCategorie(evenementCategorieVanRepo);
            _evenementCategorieRepository.Opslaan();

            //return NoContent();
            var evenementCategorieTeRetourneren = _mapper.Map<EvenementCategorieVoorRaadpleegDto>(evenementCategorieVanRepo);
            return CreatedAtRoute("GetEvenementCategorie", new { evenementCategorieId = evenementCategorieTeRetourneren.Id }, evenementCategorieTeRetourneren);
        }


        [HttpDelete("{evenementCategorieId}", Name = "VerwijderEvenementCategorie")]
        public IActionResult VerwijderEvenementCategorie(Guid evenementCategorieId)
        {
            var evenementCategorieVanRepo = _evenementCategorieRepository.GetEvenementCategorie(evenementCategorieId);
            if (evenementCategorieVanRepo == null)
            {
                return NotFound($"Evenement categorie '{evenementCategorieId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            //if (_evenementRepository.GetAantalEvenementenVanEvenementenCategorie(evenementCategorieId) > 0)
            //{
            //    return Conflict($"Er bestaan nog evenementen met als evenement categorie '{evenementCategorieId}'");
            //}
            // === EINDE Controle voor manipulatie === //

            _evenementCategorieRepository.VerwijderenEvenementCategorie(evenementCategorieVanRepo);
            _evenementCategorieRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetEvenementCategorieenOpties")]
        public IActionResult GetEvenementCategorieenOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{evenementCategorieId}", Name = "GetEvenementCategorieOpties")]
        public IActionResult GetEvenementCategorieOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
