using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class Login
    {
        [Key]
        public Guid LidId { get; set; }

        [Required]
        public string Wachtwoord { get; set; }

        public string Token { get; set; }

        //

        [ForeignKey("LidId")]
        public Lid Lid { get; set; }
    }
}
