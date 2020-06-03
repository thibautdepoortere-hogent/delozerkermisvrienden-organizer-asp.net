using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IBetaalmethodeRepository
    {
        // Betaalmethode
        bool BestaatBetaalmethode(Guid betaalmethodeId);
        bool BestaatBetaalmethode(string betaalmethodeNaam);
        bool BestaatBetaalmethodeMetUitzonderingVan(string betaalmethodeNaam, Guid betaalmethodeId);
        void ToevoegenBetaalmethode(Betaalmethode betaalmethode);
        void UpdatenBetaalmethode(Betaalmethode betaalmethode);
        void VerwijderenBetaalmethode(Betaalmethode betaalmethode);
        Betaalmethode GetBetaalmethode(Guid betaalmethodeId);
        IEnumerable<Betaalmethode> GetBetaalmethoden();
        Betaalmethode GetBetaalmethode_Overschrijving();
        Betaalmethode GetBetaalmethode_Contant();
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
