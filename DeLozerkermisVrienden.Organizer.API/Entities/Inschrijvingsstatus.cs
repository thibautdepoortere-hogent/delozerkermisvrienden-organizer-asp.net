using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Inschrijvingsstatus
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }

        [Required]
        public int Volgorde { get; set; }

        //

        public ICollection<Inschrijving> Inschrijvingen { get; set; } = new List<Inschrijving>();
    }
}
