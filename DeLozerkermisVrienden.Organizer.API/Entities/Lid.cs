using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Lid
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Voornaam { get; set; }

        [Required]
        [MaxLength(150)]
        public string Achternaam { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public bool Actief { get; set; }

        //

        public Login Login { get; set; }

        public ICollection<Betaaltransactie> Betaaltransacties { get; set; } = new List<Betaaltransactie>();

        public ICollection<Inschrijving> Inschrijvingen { get; set; } = new List<Inschrijving>();

        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();


    }
}
