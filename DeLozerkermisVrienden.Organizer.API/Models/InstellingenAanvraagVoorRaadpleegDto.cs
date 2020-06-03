using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class InstellingenAanvraagVoorRaadpleegDto
    {
        public int MinimumAantalMeter { get; set; }
        public decimal MeterPrijs { get; set; }
        public Guid AanvraagInschrijvingssstatus { get; set; }
    }
}
