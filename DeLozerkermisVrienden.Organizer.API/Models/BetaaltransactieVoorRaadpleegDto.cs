using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class BetaaltransactieVoorRaadpleegDto
    {
        public Guid Id { get; set; }

        public decimal Bedrag { get; set; }

        public string DatumBetaling { get; set; }

        public string VerantwoordelijkeBetaling { get; set; }

        public string GestructureerdeMededeling { get; set; }

        public Guid InschrijvingsId { get; set; }

        public Guid BetaalmethodeId { get; set; }

        public Guid? LidId { get; set; }
    }
}
