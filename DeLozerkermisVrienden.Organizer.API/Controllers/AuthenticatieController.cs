using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.Models;
using DeLozerkermisVrienden.Organizer.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/authenticatie")]
    public class AuthenticatieController : ControllerBase
    {
        private readonly IAuthenticatieRepository _authenticatieRepository;
        private readonly IMapper _mapper;

        public AuthenticatieController(IAuthenticatieRepository authenticatieRepository, IMapper mapper)
        {
            _authenticatieRepository = authenticatieRepository ?? throw new ArgumentNullException(nameof(authenticatieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[AllowAnonymous]
        //[HttpPost("administrator/email")]
        //public ActionResult<LoginVersleuteldWachtwoordVoorRaadpleegDto> AuthenticateAdministrator([FromBody]AuthenticatieAdministratorEmailDto authenticatieEmail)
        //{
        //    Login loginVanRepository = _authenticatieRepository.AuthenticeerAdministratorEmail(authenticatieEmail.Email);

        //    if (loginVanRepository == null)
        //    {
        //        return NotFound($"E-mail niet gevonden.");
        //    }

        //    return Ok(_mapper.Map<LoginVersleuteldWachtwoordVoorRaadpleegDto>(loginVanRepository));
        //}


        [AllowAnonymous]
        [HttpPost("administrator")]
        public ActionResult<LoginVoorRaadpleegDto> AuthenticateAdministrator([FromBody]AuthenticatieAdministratorDto authenticatie)
        {
            string token = _authenticatieRepository.AuthenticeerAdministrator(authenticatie.Email, authenticatie.Wachtwoord);

            if (string.IsNullOrWhiteSpace(token))
            {
                return NotFound($"E-mail of wachtwoord is niet correct.");
            }

            LoginVoorRaadpleegDto loginToReturn = new LoginVoorRaadpleegDto
            {
                Token = token
            };

            return Ok(loginToReturn);
        }

        [AllowAnonymous]
        [HttpPost("standhouder")]
        public ActionResult<LoginVoorRaadpleegDto> AuthenticateStandhouder([FromBody]AuthenticatieStandhouderDto authenticatie)
        {
            string token = _authenticatieRepository.AuthenticeerStandhouder(authenticatie.InschrijvingsId, authenticatie.Email);

            if (string.IsNullOrWhiteSpace(token))
            {
                return NotFound($"Inschrijvingsnummer of E-mail is niet correct.");
            }

            LoginVoorRaadpleegDto loginToReturn = new LoginVoorRaadpleegDto
            {
                Token = token
            };

            return Ok(loginToReturn);
        }
    }
}
