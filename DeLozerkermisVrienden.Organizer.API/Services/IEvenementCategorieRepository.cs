using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface IEvenementCategorieRepository
    {
        // EvenementCategorie
        bool BestaatEvenementCategorie(Guid evenementCategorieId);
        bool BestaatEvenementCategorie(string evenementCategorieNaam);
        bool BestaatEvenementCategorieMetUitzonderingVan(string evenementCategorieNaam, Guid evenementCategorieId);
        void ToevoegenEvenementCategorie(EvenementCategorie evenementCategorie);
        void UpdatenEvenementCategorie(EvenementCategorie evenementCategorie);
        void VerwijderenEvenementCategorie(EvenementCategorie evenementCategorie);
        EvenementCategorie GetEvenementCategorie(Guid evenementCategorieId);
        IEnumerable<EvenementCategorie> GetEvenementCategorieen();
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
