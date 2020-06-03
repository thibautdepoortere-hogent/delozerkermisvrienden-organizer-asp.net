using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Helpers
{
    public static class QRCode
    {
        public static string GetNieuweQRCode()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
