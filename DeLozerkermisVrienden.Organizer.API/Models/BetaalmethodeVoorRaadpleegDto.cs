using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class BetaalmethodeVoorRaadpleegDto
    {
        public Guid Id { get; set; }

        public string Naam { get; set; }

        public int AantalDagenVroegerAfsluiten { get; set; }

        public string Opmerking { get; set; }

        public int Volgorde { get; set; }
    }
}