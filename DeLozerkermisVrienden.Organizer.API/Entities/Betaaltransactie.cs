using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Betaaltransactie
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Bedrag { get; set; }

        [Required]
        public DateTime DatumBetaling { get; set; }

        [MaxLength(250)]
        public string VerantwoordelijkeBetaling { get; set; }

        [StringLength(maximumLength: 12, MinimumLength = 12)]
        public string GestructureerdeMededeling { get; set; }

        public Guid? InschrijvingsId { get; set; }

        public Guid? BetaalmethodeId { get; set; }

        public Guid? LidId { get; set; }

        //

        [ForeignKey("InschrijvingsId")]
        public Inschrijving Inschrijving { get; set; }

        [ForeignKey("BetaalmethodeId")]
        public Betaalmethode Betaalmethode { get; set; }

        [ForeignKey("LidId")]
        public Lid Lid { get; set; }

    }
}
