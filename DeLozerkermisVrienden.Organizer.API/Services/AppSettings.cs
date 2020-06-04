using Microsoft.Extensions.Hosting;
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
        private readonly bool _isDevelopment;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _isDevelopment = environment == Environments.Development;
        }

        public string ApiKey_SendGrid()
        {
            if(_isDevelopment) {
                return @"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";
            } else {
                return _configuration["ApiKeySendGrid"];
            }
        }

        public string SaltApi()
        {
            if (_isDevelopment)
            {
                return @"$2x$10$V11TbtztH5K4bi.cRHmqAO";
            }
            else
            {
                return _configuration["SaltApi"];
            }
        }

        public string JwtAuthenticationSecret()
        {
            if (_isDevelopment)
            {
                return @"2SCFx-\3M`!F,56QNd[ug%PH4q+[t<Nm!e6Jq#.!";
            }
            else
            {
                return _configuration["JWTAuthenticationSecret"];
            }
        }

        public string RekeningnummerEvenementen()
        {
            if (_isDevelopment)
            {
                return "BE68 5390 0754 7034";
            }
            else
            {
                return _configuration["RekeningnummerEvenementen"];
            }
        }

        public string EmailEvenementen()
        {
            if (_isDevelopment)
            {
                return "thibaut.depoortere@student.hogent.be";
            }
            else
            {
                return _configuration["EmailEvenementen"];
            }
        }

        public string TelefoonEvenementen()
        {
            if (_isDevelopment)
            {
                return "+32 476 12 34 56";
            }
            else
            {
                return _configuration["TelefoonEvenementen"];
            }
        }

        public bool VersturenVanAutomatischeMails()
        {
            if (_isDevelopment)
            {
                return true;
            }
            else
            {
                bool returnWaarde;
                if (bool.TryParse(_configuration["VersturenVanAutomatischeMails"], out returnWaarde))
                {
                    return returnWaarde;
                }

                return true;
            }
        }

        public int RommelmarktMinimumAantalMeter()
        {
            if (_isDevelopment)
            {
                return 3;
            }
            else
            {
                int returnWaarde;
                if (int.TryParse(_configuration["RommelmarktMinimumAantalMeter"], out returnWaarde))
                {
                    return returnWaarde;
                }

                return 0;
            }
        }

        public decimal RommelmarktMeterPrijs()
        {
            if (_isDevelopment)
            {
                return 1;
            }
            else
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
}
