using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class CheckInVoorRaadpleegDto
    {
        public Guid Id { get; set; }
        public Guid InschrijvingsId { get; set; }
        public Guid? LidId { get; set; }
        public string CheckInMoment { get; set; }
    }
}
