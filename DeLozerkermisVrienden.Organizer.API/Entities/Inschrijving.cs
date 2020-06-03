using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Inschrijving
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DatumInschrijving { get; set; }

        [Required]
        [MaxLength(150)]
        public string Voornaam { get; set; }

        [Required]
        [MaxLength(150)]
        public string Achternaam { get; set; }

        [Required]
        [StringLength(maximumLength: 4, MinimumLength = 4)]
        public string Postcode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Gemeente { get; set; }

        [Required]
        [MaxLength(6)]
        public string PrefixMobielNummer { get; set; }

        [Required]
        [StringLength(maximumLength: 9, MinimumLength = 9)]
        public string MobielNummer { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public int AantalMeter { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Meterprijs { get; set; }

        public int? AantalWagens { get; set; }

        public int? AantalAanhangwagens { get; set; }

        public int? AantalMobilhomes { get; set; }

        [MaxLength(200)]
        public string Opmerking { get; set; }

        [MaxLength(10)]
        public string Standnummer { get; set; }

        [StringLength(maximumLength: 12, MinimumLength = 12)]
        public string GestructureerdeMededeling { get; set; }

        [MaxLength(50)]
        public string QRCode { get; set; }

        [MaxLength(200)]
        public string RedenAfkeuring { get; set; }

        public Guid? InschrijvingsstatusId { get; set; }

        public Guid? BetaalmethodeId { get; set; }

        public Guid? EvenementId { get; set; }

        public Guid? LidId { get; set; }

        //

        [ForeignKey("InschrijvingsstatusId")]
        public Inschrijvingsstatus Inschrijvingsstatus { get; set; }

        [ForeignKey("BetaalmethodeId")]
        public Betaalmethode Betaalmethode { get; set; }

        [ForeignKey("EvenementId")]
        public Evenement Evenement { get; set; }

        [ForeignKey("LidId")]
        public Lid Lid { get; set; }

        public ICollection<Betaaltransactie> Betaaltransacties { get; set; } = new List<Betaaltransactie>();

        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
    }
}
