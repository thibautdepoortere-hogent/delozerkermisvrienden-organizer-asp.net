using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Evenement
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }


        public DateTime DatumStartEvenement { get; set; }

        public DateTime DatumEindeEvenement { get; set; }

        public DateTime DatumStartInschrijvingen { get; set; }

        public DateTime DatumEindeInschrijvingen { get; set; }

        public Guid? EvenementCategorieId { get; set; }

        //

        [ForeignKey("EvenementCategorieId")]
        EvenementCategorie EvenementCategorie { get; set; }

        public ICollection<Inschrijving> Inschrijvingen { get; set; } = new List<Inschrijving>();

        public ICollection<Nieuwsbrief> Nieuwsbrieven { get; set; } = new List<Nieuwsbrief>();
    }
}
