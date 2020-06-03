using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class BetaaltransactieVoorManipulatieDto
    {
        [Required(ErrorMessage = "Bedrag is verplicht.")]
        public decimal Bedrag { get; set; }

        [Required(ErrorMessage = "Datum van betaling is verplicht.")]
        public DateTime DatumBetaling { get; set; }

        [MaxLength(250, ErrorMessage = "Verantwoordelijke betaling mag niet meer dan 250 karakters bevatten.")]
        public string VerantwoordelijkeBetaling { get; set; }

        [StringLength(maximumLength: 12, ErrorMessage = "Gestructureerde mededeling moet een lengte van 12 karakters hebben.", MinimumLength = 12)]
        public string GestructureerdeMededeling { get; set; }

        [Required(ErrorMessage = "InschrijvingsId is verplicht.")]
        public Guid? InschrijvingsId { get; set; }

        [Required(ErrorMessage = "BetaalmethodeId is verplicht.")]
        public Guid? BetaalmethodeId { get; set; }

        public Guid? LidId { get; set; }
    }
}
