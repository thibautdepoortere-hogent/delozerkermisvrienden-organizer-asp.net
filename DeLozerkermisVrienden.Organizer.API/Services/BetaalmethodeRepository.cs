using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class BetaalmethodeRepository: IBetaalmethodeRepository, IDisposable
    {
        private readonly OrganizerContext _context;

        public BetaalmethodeRepository(OrganizerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
        public bool BestaatBetaalmethode(Guid betaalmethodeId)
        {
            if (betaalmethodeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(betaalmethodeId));
            }

            return _context.Betaalmethoden.Any(b => b.Id == betaalmethodeId);
        }

        public bool BestaatBetaalmethode(string betaalmethodeNaam)
        {
            if (string.IsNullOrWhiteSpace(betaalmethodeNaam))
            {
                throw new ArgumentNullException(nameof(betaalmethodeNaam));
            }

            return _context.Betaalmethoden.Any(b => b.Naam.ToUpper() == betaalmethodeNaam.ToUpper());
        }

        public bool BestaatBetaalmethodeMetUitzonderingVan(string betaalmethodeNaam, Guid betaalmethodeId)
        {
            if (string.IsNullOrWhiteSpace(betaalmethodeNaam))
            {
                throw new ArgumentNullException(nameof(betaalmethodeNaam));
            }

            if (betaalmethodeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(betaalmethodeId));
            }

            return _context.Betaalmethoden.Where(b => b.Id != betaalmethodeId).Any(b => b.Naam.ToUpper() == betaalmethodeNaam.ToUpper());
        }


        // Get (Single)
        public Betaalmethode GetBetaalmethode(Guid betaalmethodeId)
        {
            if (betaalmethodeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(betaalmethodeId));
            }

            return _context.Betaalmethoden.FirstOrDefault(b => b.Id == betaalmethodeId);
        }


        // Get (Collection)
        public IEnumerable<Betaalmethode> GetBetaalmethoden()
        {
            return _context.Betaalmethoden.OrderBy(b => b.Volgorde).ToList();
        }


        // Get (Extra)
        public Betaalmethode GetBetaalmethode_Overschrijving()
        {
            return GetBetaalmethode(Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"));
        }

        public Betaalmethode GetBetaalmethode_Contant()
        {
            return GetBetaalmethode(Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"));
        }


        // Toevoegen
        public void ToevoegenBetaalmethode(Betaalmethode betaalmethode)
        {
            if (betaalmethode == null)
            {
                throw new ArgumentNullException(nameof(betaalmethode));
            }

            if (betaalmethode.Id == Guid.Empty)
            {
                betaalmethode.Id = Guid.NewGuid();
            }

            _context.Betaalmethoden.Add(betaalmethode);
        }


        // Verwijderen
        public void VerwijderenBetaalmethode(Betaalmethode betaalmethode)
        {
            if (betaalmethode == null)
            {
                throw new ArgumentNullException(nameof(betaalmethode));
            }

            _context.Betaalmethoden.Remove(betaalmethode);
        }


        // Updaten
        public void UpdatenBetaalmethode(Betaalmethode betaalmethode)
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
            _context.Betaalmethoden.RemoveRange(GetBetaalmethoden());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten() {
            ICollection<Betaalmethode> items = new List<Betaalmethode>();
            items.Add(new Betaalmethode()
            {
                Id = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),
                Naam = "Contant",
                Opmerking = "U betaalt bij het inchecken op de dag van het evenement zelf OF U betaalt ervoor aan een lid van de vereniging.",
                Volgorde = 1
            });
            items.Add(new Betaalmethode()
            {
                Id = Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),
                Naam = "Overschrijving",
                AantalDagenVroegerAfsluiten = 5,
                Volgorde = 2
            });
            _context.Betaalmethoden.AddRange(items);
            Opslaan();
        }
    }
}
