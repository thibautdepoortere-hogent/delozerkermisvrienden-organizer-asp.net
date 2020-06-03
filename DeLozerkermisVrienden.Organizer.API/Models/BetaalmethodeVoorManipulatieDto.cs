using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class BetaalmethodeVoorManipulatieDto
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        [MaxLength(150, ErrorMessage = "Naam mag niet meer dan 150 karakters bevatten.")]
        public string Naam { get; set; }

        public int AantalDagenVroegerAfsluiten { get; set; }

        [MaxLength(150, ErrorMessage = "Opmerking mag niet meer dan 150 karakters bevatten.")]
        public string Opmerking { get; set; }

        [Required(ErrorMessage = "Volgorde is verplicht.")]
        public int? Volgorde { get; set; }
    }
}