using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Betaalmethode
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }

        public int? AantalDagenVroegerAfsluiten { get; set; }

        [MaxLength(150)]
        public string Opmerking { get; set; }

        [Required]
        public int Volgorde { get; set; }

        //

        public ICollection<Inschrijving> Inschrijvingen { get; set; } = new List<Inschrijving>();

        public ICollection<Betaaltransactie> Betaaltransacties { get; set; } = new List<Betaaltransactie>();
    }
}
