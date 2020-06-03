using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class EvenementVoorRaadpleegDto
    {
        public Guid Id { get; set; }

        public string Naam { get; set; }

        public string DatumStartEvenement { get; set; }

        public string DatumEindeEvenement { get; set; }

        public string DatumStartInschrijvingen { get; set; }

        public string DatumEindeInschrijvingen { get; set; }

        public Guid? EvenementCategorieId { get; set; }
    }
}
