using DeLozerkermisVrienden.Organizer.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    [Evenement_ControlePeriodeEvenement(ErrorMessage = "De opgegeven periode voor het evenement is ongeldig.")]
    [Evenement_ControlePeriodeInschrijving(ErrorMessage = "De opgegeven periode voor het inschrijvingmoment is ongeldig.")]
    public class EvenementVoorManipulatieDto
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        [MaxLength(150, ErrorMessage = "Naam mag niet meer dan 150 karakters bevatten.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Startdatum evenement is verplicht.")]
        public DateTime? DatumStartEvenement { get; set; }

        [Required(ErrorMessage = "Eind datum evenement is verplicht.")]
        public DateTime? DatumEindeEvenement { get; set; }

        public DateTime? DatumStartInschrijvingen { get; set; }

        public DateTime? DatumEindeInschrijvingen { get; set; }

        public Guid? EvenementCategorieId { get; set; }
    }
}
