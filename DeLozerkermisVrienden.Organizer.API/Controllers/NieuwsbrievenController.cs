using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Models;
using DeLozerkermisVrienden.Organizer.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Controllers
{
    [ApiController]
    [Route("api/nieuwsbrieven")]
    public class NieuwsbrievenController : ControllerBase
    {
        private readonly INieuwsbriefRepository _nieuwsbriefRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly IMapper _mapper;

        public NieuwsbrievenController(INieuwsbriefRepository nieuwsbriefRepository, IEvenementRepository evenementRepository,  IMapper mapper)
        {
            _nieuwsbriefRepository = nieuwsbriefRepository ?? throw new ArgumentNullException(nameof(nieuwsbriefRepository));
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost(Name = "ToevoegenNieuwsbrief")]
        public IActionResult ToevoegenNieuwsbrief([FromBody]NieuwsbriefVoorAanmaakDto nieuwsbrief)
        {
            var nieuwsbriefEntity = _mapper.Map<Entities.Nieuwsbrief>(nieuwsbrief);

            // === START Controle voor manipulatie === //
            if (nieuwsbriefEntity.EvenementId.HasValue) {
                if (!_evenementRepository.BestaatEvenement(nieuwsbriefEntity.EvenementId.Value))
                {
                    return NotFound($"Evenement '{nieuwsbriefEntity.EvenementId}' niet gevonden.");
                }
            }
                
            if (_nieuwsbriefRepository.BestaatNieuwsbrief(nieuwsbriefEntity.Email))
            {
                return Conflict($"Er bestaat reeds een nieuwsbrief met dit e-mailadres.");
            }
            // === EINDE Controle voor manipulatie === //

            _nieuwsbriefRepository.ToevoegenNieuwsbrief(nieuwsbriefEntity);
            _nieuwsbriefRepository.Opslaan();

            return Ok();
        }

    }
}
