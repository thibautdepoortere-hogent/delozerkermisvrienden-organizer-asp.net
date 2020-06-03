using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class CheckInVoorManipulatieDto
    {
        [Required]
        public Guid? InschrijvingsId { get; set; }

        [Required]
        public Guid? LidId { get; set; }
    }
}
