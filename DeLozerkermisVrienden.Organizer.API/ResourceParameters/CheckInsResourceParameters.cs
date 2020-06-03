using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.ResourceParameters
{
    public class CheckInsResourceParameters
    {
        public Guid? Inschrijving { get; set; }
        public Guid? Lid { get; set; }
        public DateTime? CheckInMomentStartPeriode { get; set; }
        public DateTime? CheckInMomentEindPeriode { get; set; }
    }
}
