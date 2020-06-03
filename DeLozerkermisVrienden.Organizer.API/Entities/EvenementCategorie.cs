using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class EvenementCategorie
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }

        //

        public ICollection<Evenement> Evenementen { get; set; } = new List<Evenement>();
    }
}
