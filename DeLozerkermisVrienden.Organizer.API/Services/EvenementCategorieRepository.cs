using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class EvenementCategorieRepository : IEvenementCategorieRepository, IDisposable
    {
        private readonly OrganizerContext _context;
        private readonly IEvenementRepository _evenementRepository;

        public EvenementCategorieRepository(OrganizerContext context, IEvenementRepository evenementRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
        }


        // Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }


        // Bestaat item
        public bool BestaatEvenementCategorie(Guid evenementCategorieId)
        {
            if (evenementCategorieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementCategorieId));
            }

            return _context.EvenementCategorieen.Any(i => i.Id == evenementCategorieId);
        }

        public bool BestaatEvenementCategorie(string evenementCategorieNaam)
        {
            if (string.IsNullOrWhiteSpace(evenementCategorieNaam))
            {
                throw new ArgumentNullException(nameof(evenementCategorieNaam));
            }

            return _context.EvenementCategorieen.Any(i => i.Naam.ToUpper() == evenementCategorieNaam.ToUpper());
        }

        public bool BestaatEvenementCategorieMetUitzonderingVan(string evenementCategorieNaam, Guid evenementCategorieId)
        {
            if (string.IsNullOrWhiteSpace(evenementCategorieNaam))
            {
                throw new ArgumentNullException(nameof(evenementCategorieNaam));
            }

            if (evenementCategorieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementCategorieId));
            }

            return _context.EvenementCategorieen.Where(i => i.Id != evenementCategorieId).Any(i => i.Naam.ToUpper() == evenementCategorieNaam.ToUpper());
        }


        // Get (Single)
        public EvenementCategorie GetEvenementCategorie(Guid evenementCategorieId)
        {
            if (evenementCategorieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementCategorieId));
            }

            return _context.EvenementCategorieen.FirstOrDefault(i => i.Id == evenementCategorieId);
        }


        // Get (Collection)
        public IEnumerable<EvenementCategorie> GetEvenementCategorieen()
        {
            return _context.EvenementCategorieen.OrderBy(i => i.Naam).ToList();
        }


        // Toevoegen
        public void ToevoegenEvenementCategorie(EvenementCategorie evenementCategorie)
        {
            if (evenementCategorie == null)
            {
                throw new ArgumentNullException(nameof(evenementCategorie));
            }

            if (evenementCategorie.Id == Guid.Empty)
            {
                evenementCategorie.Id = Guid.NewGuid();
            }

            _context.EvenementCategorieen.Add(evenementCategorie);
        }


        // Verwijderen
        public void VerwijderenEvenementCategorie(EvenementCategorie evenementCategorie)
        {
            if (evenementCategorie == null)
            {
                throw new ArgumentNullException(nameof(evenementCategorie));
            }

            if (_evenementRepository.GetAantalEvenementenVanEvenementenCategorie(evenementCategorie.Id) > 0)
            {
                _evenementRepository.VerwijderAlleEvenementenVanEvenementCategorie(evenementCategorie.Id);
            }

            _context.EvenementCategorieen.Remove(evenementCategorie);
        }


        // Updaten
        public void UpdatenEvenementCategorie(EvenementCategorie evenementCategorie)
        {
            //Not implemented
        }


        // Opslaan
        public bool Opslaan()
        {
            return (_context.SaveChanges() >= 0);
        }


        // Fabrieksinstellingen terugzetten
        public void FabrieksInstellingenVerwijderAlles()
        {
            _context.EvenementCategorieen.RemoveRange(GetEvenementCategorieen());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<EvenementCategorie> items = new List<EvenementCategorie>();
            items.Add(new EvenementCategorie()
            {
                Id = Guid.Parse("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"),
                Naam = "Lozerkermis"
            });
            _context.EvenementCategorieen.AddRange(items);
            Opslaan();
        }
    }
}
