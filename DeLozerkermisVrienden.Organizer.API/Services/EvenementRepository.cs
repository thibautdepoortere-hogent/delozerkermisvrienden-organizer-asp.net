using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class EvenementRepository : IEvenementRepository, IDisposable
    {
        private readonly OrganizerContext _context;
        private readonly IInschrijvingRepository _inschrijvingRepository;

        public EvenementRepository(OrganizerContext context, IInschrijvingRepository inschrijvingRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
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
        public bool BestaatEvenement(Guid evenementId)
        {
            if (evenementId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementId));
            }

            return _context.Evenementen.Any(i => i.Id == evenementId);
        }

        public bool BestaatEvenement(string evenementNaam)
        {
            if (string.IsNullOrWhiteSpace(evenementNaam))
            {
                throw new ArgumentNullException(nameof(evenementNaam));
            }

            return _context.Evenementen.Any(i => i.Naam.ToUpper() == evenementNaam.ToUpper());
        }

        public bool BestaatEvenementMetUitzonderingVan(string evenementNaam, Guid evenementId)
        {
            if (string.IsNullOrWhiteSpace(evenementNaam))
            {
                throw new ArgumentNullException(nameof(evenementNaam));
            }

            if (evenementId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementId));
            }

            return _context.Evenementen.Where(i => i.Id != evenementId).Any(i => i.Naam.ToUpper() == evenementNaam.ToUpper());
        }


        // Get (Single)
        public Evenement GetEvenement(Guid evenementId)
        {
            if (evenementId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementId));
            }

            return _context.Evenementen.FirstOrDefault(i => i.Id == evenementId);
        }


        // Get (Collection)
        public IEnumerable<Evenement> GetEvenementen()
        {
            return _context.Evenementen.OrderBy(i => i.Naam).ToList();
        }

        public IEnumerable<Evenement> GetEvenementen(EvenementenResourceParameters evenementenResourceParameters)
        {
            if (evenementenResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(evenementenResourceParameters));
            }

            if (!evenementenResourceParameters.StartPeriode.HasValue && !evenementenResourceParameters.EindPeriode.HasValue && !evenementenResourceParameters.EvenementCategorie.HasValue)
            {
                return GetEvenementen();
            }

            if (evenementenResourceParameters.StartPeriode.HasValue && evenementenResourceParameters.EindPeriode.HasValue)
            {
                if (evenementenResourceParameters.StartPeriode > evenementenResourceParameters.EindPeriode)
                {
                    throw new ArgumentException("De opgegeven periode is ongeldig.");
                }
            }

            if (!evenementenResourceParameters.EvenementCategorie.HasValue)
            {
                if (evenementenResourceParameters.EvenementCategorie == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(evenementenResourceParameters.EvenementCategorie));
                }
            }

            var collectie = _context.Evenementen as IQueryable<Evenement>;
            
            if (evenementenResourceParameters.StartPeriode.HasValue)
            {
                collectie = collectie.Where(e => e.DatumStartEvenement >= evenementenResourceParameters.StartPeriode | e.DatumEindeEvenement >= evenementenResourceParameters.StartPeriode);
            }

            if (evenementenResourceParameters.EindPeriode.HasValue)
            {
                collectie = collectie.Where(e => e.DatumEindeEvenement <= evenementenResourceParameters.EindPeriode | e.DatumStartEvenement <= evenementenResourceParameters.EindPeriode);
            }

            if (evenementenResourceParameters.EvenementCategorie.HasValue)
            {
                collectie = collectie.Where(e => e.EvenementCategorieId == evenementenResourceParameters.EvenementCategorie);
            }

            return collectie.OrderBy(e => e.DatumStartEvenement).ToList();
        }


        // Get (Extra)
        public int GetAantalEvenementenVanEvenementenCategorie(Guid evenementCategorieId)
        {
            return _context.Evenementen.Count(e => e.EvenementCategorieId == evenementCategorieId);
        }


        // Toevoegen
        public void ToevoegenEvenement(Evenement evenement)
        {
            if (evenement == null)
            {
                throw new ArgumentNullException(nameof(evenement));
            }

            if (evenement.Id == Guid.Empty)
            {
                evenement.Id = Guid.NewGuid();
            }

            _context.Evenementen.Add(evenement);
        }


        // Verwijderen
        public void VerwijderenEvenement(Evenement evenement)
        {
            if (evenement == null)
            {
                throw new ArgumentNullException(nameof(evenement));
            }

            if (_inschrijvingRepository.GetAantalInschrijvingenVanEvenement(evenement.Id) > 0)
            {
                _inschrijvingRepository.VerwijderAlleInschrijvingenVanEvenement(evenement.Id);
            }

            _context.Evenementen.Remove(evenement);
        }

        public void VerwijderAlleEvenementenVanEvenementCategorie(Guid evenementCategorieId)
        {
            if (evenementCategorieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementCategorieId));
            }

            IEnumerable<Evenement> teVerwijderenEvenementen = _context.Evenementen.Where(e => e.EvenementCategorieId == evenementCategorieId);
            foreach (Evenement evenement in teVerwijderenEvenementen)
            {
                VerwijderenEvenement(evenement);
            }
        }


        // Updaten
        public void UpdatenEvenement(Evenement evenement)
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
            _context.Evenementen.RemoveRange(GetEvenementen());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Evenement> items = new List<Evenement>();
            items.Add(new Evenement()
            {
                Id = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e"),
                Naam = "Rommelmarkt 2020",
                DatumStartEvenement = new DateTime(2020, 9, 26),
                DatumEindeEvenement = new DateTime(2020, 9, 26),
                DatumStartInschrijvingen = new DateTime(2020, 6, 26),
                DatumEindeInschrijvingen = new DateTime(2020, 9, 23),
                EvenementCategorieId = Guid.Parse("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1")   // Lozerkermis
            });
            _context.Evenementen.AddRange(items);
            Opslaan();
        }
    }
}
