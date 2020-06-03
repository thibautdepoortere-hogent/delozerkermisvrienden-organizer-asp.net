using DeLozerkermisVrienden.Organizer.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class InschrijvingVoorManipulatieDto
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [MaxLength(150, ErrorMessage = "Voonraam mag niet meer dan 150 karakters bevatten.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [MaxLength(150, ErrorMessage = "Achternaam mag niet meer dan 150 karakters bevatten.")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht.")]
        [StringLength(maximumLength: 4, ErrorMessage = "Postcode moet een lengte van 4 karakters hebben.", MinimumLength = 4)]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Gemeente is verplicht.")]
        [MaxLength(100, ErrorMessage = "Gemeente mag niet meer dan 100 karakters bevatten.")]
        public string Gemeente { get; set; }

        [Required(ErrorMessage = "Prefix voor mobiel nummer is verplicht.")]
        [MaxLength(6, ErrorMessage = "Prefix voor mobiel nummer mag niet meer dan 6 karakters bevatten.")]
        public string PrefixMobielNummer { get; set; }

        [Required(ErrorMessage = "Mobiel nummer is verplicht.")]
        [StringLength(maximumLength: 9, ErrorMessage = "Mobiel nummer moet een lengte van 9 karakters hebben.", MinimumLength = 9)]
        public string MobielNummer { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [MaxLength(200, ErrorMessage = "E-mailadres mag niet meer dan 200 karakters bevatten.")]
        [EmailAddress(ErrorMessage = "Er dient een geldig e - mailadres opgegeven te worden.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Aantal meter is verplicht.")]
        public int AantalMeter { get; set; }

        public int? AantalWagens { get; set; }

        public int? AantalAanhangwagens { get; set; }

        public int? AantalMobilhomes { get; set; }

        [MaxLength(200, ErrorMessage = "Opmerking mag niet meer dan 200 karakters bevatten.")]
        public string Opmerking { get; set; }

        [MaxLength(10, ErrorMessage = "Standnummer mag niet meer dan 10 karakters bevatten.")]
        public string Standnummer { get; set; }

        [StringLength(maximumLength: 12, ErrorMessage = "Gestructureerde mededeling moet een lengte van 12 karakters hebben.", MinimumLength = 12)]
        public string GestructureerdeMededeling { get; set; }

        [MaxLength(50, ErrorMessage = "QRCode mag niet meer dan 50 karakters bevatten.")]
        public string QRCode { get; set; }

        [MaxLength(200, ErrorMessage = "Reden voor afkeuring mag niet meer dan 200 karakters bevatten.")]
        public string RedenAfkeuring { get; set; }

        [Required(ErrorMessage = "Inschrijvingsstatus is verplicht.")]
        public Guid InschrijvingsstatusId { get; set; }

        [Required(ErrorMessage = "Betaalmethode is verplicht.")]
        public Guid BetaalmethodeId { get; set; }

        [Required(ErrorMessage = "Evenement is verplicht.")]
        public Guid EvenementId { get; set; }

        public Guid? LidId { get; set; }
    }
}
