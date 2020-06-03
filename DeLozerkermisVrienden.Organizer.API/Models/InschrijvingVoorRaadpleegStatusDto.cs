using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class InschrijvingVoorRaadpleegStatusDto
    {
        public Guid Id { get; set; }

        public string DatumInschrijving { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public string Postcode { get; set; }

        public string Gemeente { get; set; }

        public string PrefixMobielNummer { get; set; }

        public string MobielNummer { get; set; }

        public string Email { get; set; }

        public int AantalMeter { get; set; }

        public decimal MeterPrijs { get; set; }

        public int? AantalWagens { get; set; }

        public int? AantalAanhangwagens { get; set; }

        public int? AantalMobilhomes { get; set; }

        public string Opmerking { get; set; }

        public string GestructureerdeMededeling { get; set; }

        public string QRCode { get; set; }

        public string RedenAfkeuring { get; set; }

        public Guid? InschrijvingsstatusId { get; set; }

        public Guid? BetaalmethodeId { get; set; }

        public Guid EvenementId { get; set; }
    }
}
