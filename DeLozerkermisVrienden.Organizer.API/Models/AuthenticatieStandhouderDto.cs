using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class AuthenticatieStandhouderDto
    {
        [Required(ErrorMessage = "InschrijvingsId is verplicht.")]
        public string InschrijvingsId { get; set; }

        [Required(ErrorMessage = "E-mail is verplicht.")]
        [EmailAddress(ErrorMessage = "Er dient een geldig e-mailadres opgegeven te worden.")]
        public string Email { get; set; }
    }
}
