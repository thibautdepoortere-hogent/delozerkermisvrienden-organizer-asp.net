using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IEvenementRepository
    {
        // Evenement
        bool BestaatEvenement(Guid evenementId);
        bool BestaatEvenement(string evenementNaam);
        bool BestaatEvenementMetUitzonderingVan(string evenementNaam, Guid evenementId);
        void ToevoegenEvenement(Evenement evenement);
        void UpdatenEvenement(Evenement evenement);
        void VerwijderenEvenement(Evenement evenement);
        void VerwijderAlleEvenementenVanEvenementCategorie(Guid evenementCategorieId);
        Evenement GetEvenement(Guid evenementId);
        IEnumerable<Evenement> GetEvenementen();
        IEnumerable<Evenement> GetEvenementen(EvenementenResourceParameters evenementenResourceParameters);
        int GetAantalEvenementenVanEvenementenCategorie(Guid evenementCategorieId);
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
