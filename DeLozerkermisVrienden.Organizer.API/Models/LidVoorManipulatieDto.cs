using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class LidVoorManipulatieDto
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [MaxLength(150, ErrorMessage = "Voornaam mag niet meer dan 150 karakters bevatten.")]
        public string Voornaam { get; set; }
        
        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [MaxLength(150, ErrorMessage = "Achternaam mag niet meer dan 150 karakters bevatten.")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Email is verplicht.")]
        [EmailAddress(ErrorMessage = "Er dient een geldig e-mailadres opgegeven te worden.")]
        public string Email { get; set; }

        public bool Actief { get; set; }
    }
}
