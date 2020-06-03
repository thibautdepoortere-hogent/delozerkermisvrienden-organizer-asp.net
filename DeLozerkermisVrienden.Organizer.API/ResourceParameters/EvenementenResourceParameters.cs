using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.ResourceParameters
{
    public class EvenementenResourceParameters
    {
        public DateTime? StartPeriode { get; set; }
        public DateTime? EindPeriode { get; set; }
        public Guid? EvenementCategorie { get; set; }
    }
}
