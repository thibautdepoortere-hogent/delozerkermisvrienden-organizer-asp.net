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
    [Route("api/checkins")]
    public class CheckInsController : ControllerBase
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly ILidRepository _lidRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly IMapper _mapper;

        public CheckInsController(ICheckInRepository checkInRepository, ILidRepository lidRepository, IInschrijvingRepository inschrijvingRepository, IMapper mapper)
        {
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(checkInRepository));
            _lidRepository = lidRepository ?? throw new ArgumentNullException(nameof(lidRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet(Name = "GetCheckIns")]
        public ActionResult<IEnumerable<CheckInVoorRaadpleegDto>> GetCheckIns([FromQuery] CheckInsResourceParameters checkInsResourceParameters)
        {
            if (checkInsResourceParameters != null)
            {
                if (checkInsResourceParameters.Inschrijving.HasValue)
                {
                    if (!_inschrijvingRepository.BestaatInschrijving(checkInsResourceParameters.Inschrijving.Value))
                    {
                        return NotFound($"Inschrijving '{checkInsResourceParameters.Inschrijving.Value}' niet gevonden.");
                    }
                }

                if (checkInsResourceParameters.Lid.HasValue)
                {
                    if (!_lidRepository.BestaatLid(checkInsResourceParameters.Lid.Value))
                    {
                        return NotFound($"Lid '{checkInsResourceParameters.Lid.Value}' niet gevonden.");
                    }
                }

                if (checkInsResourceParameters.CheckInMomentStartPeriode.HasValue && checkInsResourceParameters.CheckInMomentEindPeriode.HasValue)
                {
                    if (checkInsResourceParameters.CheckInMomentStartPeriode > checkInsResourceParameters.CheckInMomentEindPeriode)
                    {
                        return BadRequest("De opgegeven periode is ongeldig.");
                    }
                }
            }

            var checkInsVanRepo = _checkInRepository.GetCheckIns(checkInsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<CheckInVoorRaadpleegDto>>(checkInsVanRepo));
        }


        [HttpHead("{checkInId}")]
        [HttpGet("{checkInId}", Name = "GetCheckIn")]
        public IActionResult GetCheckIn(Guid checkInId)
        {
            var checkInVanRepo = _checkInRepository.GetCheckIn(checkInId);
            if (checkInVanRepo == null)
            {
                return NotFound($"Check-in '{checkInId}' niet gevonden.");
            }
            return Ok(_mapper.Map<CheckInVoorRaadpleegDto>(checkInVanRepo));
        }


        [HttpPost(Name = "ToevoegenCheckIn")]
        public ActionResult<CheckInVoorRaadpleegDto> ToevoegenCheckIn([FromBody]CheckInVoorAanmaakDto checkIn)
        {
            var checkInEntity = _mapper.Map<Entities.CheckIn>(checkIn);
            checkInEntity.CheckInMoment = DateTime.UtcNow.AddHours(2);

            // === START Controle voor manipulatie === //
            if (!_inschrijvingRepository.BestaatInschrijving(checkInEntity.InschrijvingsId.Value))
            {
                return NotFound($"Inschrijving '{checkInEntity.InschrijvingsId}' niet gevonden.");
            }

            if (!_lidRepository.BestaatLid(checkInEntity.LidId.Value))
            {
                return NotFound($"Lid '{checkInEntity.LidId}' niet gevonden.");
            }
            // === EINDE Controle voor manipulatie === //

            _checkInRepository.ToevoegenCheckIn(checkInEntity);
            _checkInRepository.Opslaan();

            var checkInTeRetourneren = _mapper.Map<CheckInVoorRaadpleegDto>(checkInEntity);
            return CreatedAtRoute("GetCheckIn", new { checkInId = checkInTeRetourneren.Id }, checkInTeRetourneren);
        }


        [HttpPut("{checkInId}", Name = "VolledigeUpdateCheckIn")]
        public IActionResult VolledigeUpdateCheckIn(Guid checkInId, [FromBody]CheckInVoorUpdateDto checkIn)
        {
            var checkInVanRepo = _checkInRepository.GetCheckIn(checkInId);
            if (checkInVanRepo == null)
            {
                return NotFound($"Check-in '{checkInId}' niet gevonden.");
            }

            _mapper.Map(checkIn, checkInVanRepo);

            // === START Controle voor manipulatie === //
            if (!_inschrijvingRepository.BestaatInschrijving(checkInVanRepo.InschrijvingsId.Value))
            {
                return NotFound($"Inschrijving '{checkInVanRepo.InschrijvingsId}' niet gevonden.");
            }

            if (!_lidRepository.BestaatLid(checkInVanRepo.LidId.Value))
            {
                return NotFound($"Lid '{checkInVanRepo.LidId}' niet gevonden.");
            }
            // === EINDE Controle voor manipulatie === //

            _checkInRepository.UpdatenCheckIn(checkInVanRepo);
            _checkInRepository.Opslaan();

            //return NoContent();
            var checkInTeRetourneren = _mapper.Map<CheckInVoorRaadpleegDto>(checkInVanRepo);
            return CreatedAtRoute("GetCheckIn", new { checkInId = checkInTeRetourneren.Id }, checkInTeRetourneren);
        }


        [HttpPatch("{checkInId}", Name = "GedeeltelijkeUpdateCheckIn")]
        public IActionResult GedeeltelijkeUpdateCheckIn(Guid checkInId, [FromBody]JsonPatchDocument<CheckInVoorUpdateDto> patchDocument)
        {
            var checkInVanRepo = _checkInRepository.GetCheckIn(checkInId);
            if (checkInVanRepo == null)
            {
                return NotFound($"CheckIn '{checkInId}' niet gevonden.");
            }

            var checkInTePatchen = _mapper.Map<CheckInVoorUpdateDto>(checkInVanRepo);
            patchDocument.ApplyTo(checkInTePatchen, ModelState);

            if (!TryValidateModel(checkInTePatchen))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(checkInTePatchen, checkInVanRepo);

            // === START Controle voor manipulatie === //
            if (!_inschrijvingRepository.BestaatInschrijving(checkInVanRepo.InschrijvingsId.Value))
            {
                return NotFound($"Inschrijving '{checkInVanRepo.InschrijvingsId}' niet gevonden.");
            }

            if (!_lidRepository.BestaatLid(checkInVanRepo.LidId.Value))
            {
                return NotFound($"Lid '{checkInVanRepo.LidId}' niet gevonden.");
            }
            // === EINDE Controle voor manipulatie === //

            _checkInRepository.UpdatenCheckIn(checkInVanRepo);
            _checkInRepository.Opslaan();

            //return NoContent();
            var checkInTeRetourneren = _mapper.Map<CheckInVoorRaadpleegDto>(checkInVanRepo);
            return CreatedAtRoute("GetCheckIn", new { checkInId = checkInTeRetourneren.Id }, checkInTeRetourneren);
        }


        [HttpDelete("{checkInId}", Name = "VerwijderCheckIn")]
        public IActionResult VerwijderCheckIn(Guid checkInId)
        {
            var checkInVanRepo = _checkInRepository.GetCheckIn(checkInId);
            if (checkInVanRepo == null)
            {
                return NotFound($"CheckIn '{checkInId}' niet gevonden.");
            }

            // === START Controle voor manipulatie === //
            
            // === EINDE Controle voor manipulatie === //

            _checkInRepository.VerwijderenCheckIn(checkInVanRepo);
            _checkInRepository.Opslaan();

            return NoContent();
        }


        [HttpOptions(Name = "GetCheckInsOpties")]
        public IActionResult GetCheckInsOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }


        [HttpOptions("{checkInId}", Name = "GetCheckInOpties")]
        public IActionResult GetCheckInOpties()
        {
            Response.Headers.Add("Allow", "OPTIONS, HEAD, GET, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}
