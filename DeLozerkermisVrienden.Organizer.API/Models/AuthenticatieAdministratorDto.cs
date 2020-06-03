using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class AuthenticatieAdministratorDto
    {
        [Required(ErrorMessage = "E-mail is verplicht.")]
        [EmailAddress(ErrorMessage = "Er dient een geldig e-mailadres opgegeven te worden.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        public string Wachtwoord { get; set; }
    }
}
