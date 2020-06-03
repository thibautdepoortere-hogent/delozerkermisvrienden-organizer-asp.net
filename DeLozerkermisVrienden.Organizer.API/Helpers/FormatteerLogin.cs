using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Helpers
{
    public static class FormatteerLogin
    {
        public static Login ZonderWachtwoord(this Login login)
        {
            login.Wachtwoord = null;
            return login;
        }
    }
}
