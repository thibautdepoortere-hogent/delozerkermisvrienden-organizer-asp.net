using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class InschrijvingVoorAanmaakDto : InschrijvingVoorManipulatieDto
    {
        [Required(ErrorMessage = "Meterprijs is verplicht.")]
        public decimal Meterprijs { get; set; }
    }
}
