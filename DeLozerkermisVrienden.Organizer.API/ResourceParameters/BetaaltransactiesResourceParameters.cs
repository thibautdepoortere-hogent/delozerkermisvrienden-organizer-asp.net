using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.ResourceParameters
{
    public class BetaaltransactiesResourceParameters
    {
        public string VerantwoordelijkeBetaling { get; set; }
        public string GestructureerdeMededeling { get; set; }
        public Guid? Inschrijving { get; set; }
        public Guid? Betaalmethode { get; set; }
        public Guid? Lid { get; set; }
    }
}
