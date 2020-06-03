using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IBetaaltransactieRepository
    {
        // Betaaltransactie
        bool BestaatBetaaltransactie(Guid betaaltransactiesId);
        void ToevoegenBetaaltransactie(Betaaltransactie betaaltransactie);
        void UpdatenBetaaltransactie(Betaaltransactie betaaltransactie);
        void VerwijderenBetaaltransactie(Betaaltransactie betaaltransactie);
        void VerwijderAlleBetaaltransactiesVanInschrijving(Guid inschrijvingsId);
        Betaaltransactie GetBetaaltransactie(Guid betaaltransactiesId);
        IEnumerable<Betaaltransactie> GetBetaaltransacties();
        IEnumerable<Betaaltransactie> GetBetaaltransacties(BetaaltransactiesResourceParameters betaaltransactiesResourceParameters);
        int GetAantalBetaaltransactiesVanInschrijving(Guid inschrijvingsId);
        int GetAantalBetaaltransactiesVanBetaalmethode(Guid betaalmethodeId);
        int GetAantalBetaaltransactiesVanLid(Guid lidId);
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
