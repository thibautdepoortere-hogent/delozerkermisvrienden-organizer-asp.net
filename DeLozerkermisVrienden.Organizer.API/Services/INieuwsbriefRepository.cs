using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface INieuwsbriefRepository
    {
        bool BestaatNieuwsbrief(Guid nieuwsbriefId);
        bool BestaatNieuwsbrief(string email);
        IEnumerable<Nieuwsbrief> GetNieuwsbrieven();
        void ToevoegenNieuwsbrief(Nieuwsbrief nieuwsbrief);
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
