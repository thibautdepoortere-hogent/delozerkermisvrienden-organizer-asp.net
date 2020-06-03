using DeLozerkermisVrienden.Organizer.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.ResourceParameters
{
    public class InschrijvingenResourceParameters
    {
        public string Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Standnummer { get; set; }
        public string GestructureerdeMededeling { get; set; }
        public string QRCode { get; set; }
        public bool? NogNietIngecheckt { get; set; }
        public Guid? Inschrijvingsstatus { get; set; }
        public Guid? Evenement { get; set; }
        public Sorteer.SorteerMethode? SorteerMethode { get; set; }
    }
}
