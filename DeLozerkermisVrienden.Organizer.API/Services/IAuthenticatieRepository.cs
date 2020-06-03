using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IAuthenticatieRepository
    {
        Login AuthenticeerAdministratorEmail(string email);
        string AuthenticeerAdministrator(string email, string wachtwoord);
        string AuthenticeerStandhouder(string inschrijvingsId, string email);
    }
}
