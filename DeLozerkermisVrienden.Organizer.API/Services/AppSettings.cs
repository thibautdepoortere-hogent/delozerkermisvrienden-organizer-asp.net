using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string ApiKey_SendGrid()
        {
            return _configuration["ApiKeySendGrid"];
        }

        public string JwtAuthenticationSecret()
        {
            return _configuration["JWTAuthenticationSecret"];
        }

        public string RekeningnummerEvenementen()
        {
            return _configuration["RekeningnummerEvenementen"];
        }

        public string EmailEvenementen()
        {
            return _configuration["EmailEvenementen"];
        }

        public string TelefoonEvenementen()
        {
            return _configuration["TelefoonEvenementen"];
        }

        public bool VersturenVanAutomatischeMails()
        {
            bool returnWaarde;
            if (bool.TryParse(_configuration["VersturenVanAutomatischeMails"], out returnWaarde))
            {
                return returnWaarde;
            }

            return true;
        }

        public int RommelmarktMinimumAantalMeter()
        {
            int returnWaarde;
            if (int.TryParse(_configuration["RommelmarktMinimumAantalMeter"], out returnWaarde))
            {
                return returnWaarde;
            }

            return 0;
        }

        public decimal RommelmarktMeterPrijs()
        {
            decimal returnWaarde;
            if (decimal.TryParse(_configuration["RommelmarktMeterPrijs"], out returnWaarde))
            {
                return returnWaarde;
            }

            return 0;
        }
    }
}
