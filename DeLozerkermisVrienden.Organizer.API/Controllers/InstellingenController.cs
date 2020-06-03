using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Services;
using DeLozerkermisVrienden.Organizer.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Controllers
{

    [Route("api/instellingen")]
    public class InstellingenController : ControllerBase
    {
        private readonly IInschrijvingsstatusRepository _inschrijvingsstatusRepository;
        private readonly IMapper _mapper;
        private readonly int _minimumAantalMeter;
        private readonly decimal _meterPrijs;

        public InstellingenController(IInschrijvingsstatusRepository inschrijvingsstatusRepository, IAppSettings appSettings, IMapper mapper)
        {
            _inschrijvingsstatusRepository = inschrijvingsstatusRepository ?? throw new ArgumentNullException(nameof(inschrijvingsstatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            _minimumAantalMeter = appSettings.RommelmarktMinimumAantalMeter();
            _meterPrijs = appSettings.RommelmarktMeterPrijs();
        }


        [HttpGet("aanvraag", Name = "GetInstellingen_Aanvraag")]
        public ActionResult<InstellingenAanvraagVoorRaadpleegDto> GetInstellingen_Aanvraag()
        {
            var instellingenAanvraag = new Entities.InstellingenAanvraag();
            instellingenAanvraag.AanvraagInschrijvingssstatus = _inschrijvingsstatusRepository.GetInschrijvingsstatus_Aangevraagd().Id;
            instellingenAanvraag.MeterPrijs = _meterPrijs;
            instellingenAanvraag.MinimumAantalMeter = _minimumAantalMeter;

            return Ok(_mapper.Map<InstellingenAanvraagVoorRaadpleegDto>(instellingenAanvraag));
        }
    }
}
