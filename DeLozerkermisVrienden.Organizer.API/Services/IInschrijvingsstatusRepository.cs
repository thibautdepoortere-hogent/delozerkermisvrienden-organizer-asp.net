using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IInschrijvingsstatusRepository
    {
        // Inschrijvingsstatus
        bool BestaatInschrijvingsstatus(Guid inschrijvingsstatusId);
        bool BestaatInschrijvingsstatus(string inschrijvingsstatusNaam);
        bool BestaatInschrijvingsstatusMetUitzonderingVan(string inschrijvingsstatusNaam, Guid inschrijvingsstatusId);
        void ToevoegenInschrijvingsstatus(Inschrijvingsstatus inschrijvingsstatus);
        void UpdatenInschrijvingsstatus(Inschrijvingsstatus inschrijvingsstatus);
        void VerwijderenInschrijvingsstatus(Inschrijvingsstatus inschrijvingsstatus);
        Inschrijvingsstatus GetInschrijvingsstatus(Guid inschrijvingsstatusId);
        Inschrijvingsstatus GetInschrijvingsstatus_Aangevraagd();
        Inschrijvingsstatus GetInschrijvingsstatus_Goedgekeurd();
        Inschrijvingsstatus GetInschrijvingsstatus_Gepland();
        Inschrijvingsstatus GetInschrijvingsstatus_Afgekeurd();
        IEnumerable<Inschrijvingsstatus> GetInschrijvingsstatussen();
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
