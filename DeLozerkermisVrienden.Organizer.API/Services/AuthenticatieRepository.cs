using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DeLozerkermisVrienden.Organizer.API.Helpers;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class AuthenticatieRepository : IAuthenticatieRepository, IDisposable
    {
        private readonly OrganizerContext _context;
        private readonly ILidRepository _lidRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly string _jwtAuthenticationSecret;
        private readonly string _salt;

        public AuthenticatieRepository(OrganizerContext context, ILidRepository lidRepository, ILoginRepository loginRepository, IInschrijvingRepository inschrijvingsRepository, IAppSettings appSettings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _lidRepository = lidRepository ?? throw new ArgumentNullException(nameof(lidRepository));
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _inschrijvingRepository = inschrijvingsRepository ?? throw new ArgumentNullException(nameof(inschrijvingsRepository));
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            _jwtAuthenticationSecret = appSettings.JwtAuthenticationSecret();
            _salt = appSettings.SaltApi();
        }


        // Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public Login AuthenticeerAdministratorEmail(string email)
        {
            Lid lidVanRepo = _lidRepository.GetLid(email);
            if (lidVanRepo == null)
            {
                return null;
            }

            Login loginVanRepo = _loginRepository.GetLogin(lidVanRepo.Id);
            if (loginVanRepo == null)
            {
                return null;
            }

            return loginVanRepo;
        }


        public string AuthenticeerAdministrator(string email, string wachtwoord)
        {
            Lid lidVanRepo = _lidRepository.GetLid(email);
            if (lidVanRepo == null)
            {
                return null;
            }

            Login loginVanRepo = _loginRepository.GetLogin(lidVanRepo.Id);
            if (loginVanRepo == null)
            {
                return null;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(wachtwoord, _salt);
            if (loginVanRepo.Wachtwoord == hashedPassword)
            {
                return GenereerToken(loginVanRepo.LidId.ToString(), loginVanRepo.Lid.Voornaam.ToString(), "Administrator", 7);
            }
            else
            {
                return null;
            }
        }

        public string AuthenticeerStandhouder(string inschrijvingsIdTekstFormaat, string email)
        {
            Guid inschrijvingsId;
            if (!Guid.TryParse(inschrijvingsIdTekstFormaat, out inschrijvingsId))
            {
                return null;
            }

            Inschrijving inschrijvingVanRepo = _inschrijvingRepository.GetInschrijving(inschrijvingsId);
            if (inschrijvingVanRepo == null)
            {
                return null;
            }

            if (inschrijvingVanRepo.Email.ToUpper() == email.ToUpper())
            {
                return GenereerToken(inschrijvingVanRepo.Id.ToString(), inschrijvingVanRepo.Voornaam.ToString(), "Standhouder", 1);
            }
            else
            {
                return null;
            }
        }



        private string GenereerToken(string id, string voornaam, string rol, int aantalDagenGeldig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtAuthenticationSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier,id),
                        new Claim(
                            ClaimTypes.GivenName,
                            voornaam
                        ),
                        new Claim(
                            ClaimTypes.Role,
                            rol
                        )
                }),
                Expires = DateTime.UtcNow.AddDays(aantalDagenGeldig),
                NotBefore = DateTime.UtcNow,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
