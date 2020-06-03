using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class NieuwsbriefVoorManipulatieDto
    {
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public Guid? EvenementId { get; set; }
    }
}
