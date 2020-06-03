using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Nieuwsbrief
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public Guid? EvenementId { get; set; }

        //

        [ForeignKey("EvenementId")]
        public Evenement Evenement { get; set; }
    }
}
