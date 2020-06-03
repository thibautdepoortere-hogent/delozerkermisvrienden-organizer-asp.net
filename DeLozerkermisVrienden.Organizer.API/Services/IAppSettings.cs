using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IAppSettings
    {
        string ApiKey_SendGrid();
        string JwtAuthenticationSecret();
        string RekeningnummerEvenementen();
        string EmailEvenementen();
        string TelefoonEvenementen();
        bool VersturenVanAutomatischeMails();
        int RommelmarktMinimumAantalMeter();
        decimal RommelmarktMeterPrijs();
    }
}
