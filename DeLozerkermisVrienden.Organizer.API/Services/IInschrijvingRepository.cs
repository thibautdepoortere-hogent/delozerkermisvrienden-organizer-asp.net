using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IInschrijvingRepository
    {
        // Inschrijving
        bool BestaatInschrijving(Guid inschrijvingsId);
        void ToevoegenInschrijving(Inschrijving inschrijving);
        void UpdatenInschrijving(Inschrijving inschrijving);
        void VerwijderenInschrijving(Inschrijving inschrijving);
        void VerwijderAlleInschrijvingenVanEvenement(Guid evenementId);
        Inschrijving GetInschrijving(Guid inschrijvingsId);
        IEnumerable<Inschrijving> GetInschrijvingen();
        IEnumerable<Inschrijving> GetInschrijvingen(InschrijvingenResourceParameters inschrijvingenResourceParameters);
        int GetAantalInschrijvingenVanBetaalmethode(Guid betaalmethodeId);
        int GetAantalInschrijvingenVanInschrijvingsstatus(Guid inschrijvingsstatusId);
        int GetAantalInschrijvingenVanEvenement(Guid evenementId);
        int GetAantalInschrijvingenVanLid(Guid lidId);
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
