using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IMailing
    {
        void VerstuurMail_NieuweAanvraag(Entities.Inschrijving inschrijving);
        void VerstuurMail_AanvraagGoedgekeurd(Entities.Inschrijving inschrijving);
        void VerstuurMail_AanvraagAfgekeurd(Entities.Inschrijving inschrijving);
    }
}
