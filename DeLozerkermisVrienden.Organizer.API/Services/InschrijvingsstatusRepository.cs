using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class InschrijvingsstatusRepository : IInschrijvingsstatusRepository, IDisposable
    {
        private readonly OrganizerContext _context;

        public InschrijvingsstatusRepository(OrganizerContext context)
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
        public bool BestaatInschrijvingsstatus(Guid inschrijvingsstatusId)
        {
            if (inschrijvingsstatusId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatusId));
            }

            return _context.Inschrijvingsstatussen.Any(i => i.Id == inschrijvingsstatusId);
        }

        public bool BestaatInschrijvingsstatus(string inschrijvingsstatusNaam)
        {
            if (string.IsNullOrWhiteSpace(inschrijvingsstatusNaam))
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatusNaam));
            }

            return _context.Inschrijvingsstatussen.Any(i => i.Naam.ToUpper() == inschrijvingsstatusNaam.ToUpper());
        }

        public bool BestaatInschrijvingsstatusMetUitzonderingVan(string inschrijvingsstatusNaam, Guid inschrijvingsstatusId)
        {
            if (string.IsNullOrWhiteSpace(inschrijvingsstatusNaam))
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatusNaam));
            }

            if (inschrijvingsstatusId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatusId));
            }

            return _context.Inschrijvingsstatussen.Where(i => i.Id != inschrijvingsstatusId).Any(i => i.Naam.ToUpper() == inschrijvingsstatusNaam.ToUpper());
        }


        // Get (Single)
        public Inschrijvingsstatus GetInschrijvingsstatus(Guid inschrijvingsstatusId)
        {
            if (inschrijvingsstatusId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatusId));
            }

            return _context.Inschrijvingsstatussen.FirstOrDefault(i => i.Id == inschrijvingsstatusId);
        }


        // Get (Collection)
        public IEnumerable<Inschrijvingsstatus> GetInschrijvingsstatussen()
        {
            return _context.Inschrijvingsstatussen.OrderBy(i => i.Volgorde).ToList();
        }


        // Get (Extra)
        public Inschrijvingsstatus GetInschrijvingsstatus_Aangevraagd()
        {
            return GetInschrijvingsstatus(Guid.Parse("4c83bb5b-30b2-4ac7-9662-eab035157b86"));
        }

        public Inschrijvingsstatus GetInschrijvingsstatus_Goedgekeurd()
        {
            return GetInschrijvingsstatus(Guid.Parse("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"));
        }

        public Inschrijvingsstatus GetInschrijvingsstatus_Gepland()
        {
            return GetInschrijvingsstatus(Guid.Parse("adb494b6-10ae-495a-9ba4-48ef04d0e29f"));
        }

        public Inschrijvingsstatus GetInschrijvingsstatus_Afgekeurd()
        {
            return GetInschrijvingsstatus(Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"));
        }


        // Toevoegen
        public void ToevoegenInschrijvingsstatus(Inschrijvingsstatus inschrijvingsstatus)
        {
            if (inschrijvingsstatus == null)
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatus));
            }

            if (inschrijvingsstatus.Id == Guid.Empty)
            {
                inschrijvingsstatus.Id = Guid.NewGuid();
            }

            _context.Inschrijvingsstatussen.Add(inschrijvingsstatus);
        }


        // Verwijderen
        public void VerwijderenInschrijvingsstatus(Inschrijvingsstatus inschrijvingsstatus)
        {
            if (inschrijvingsstatus == null)
            {
                throw new ArgumentNullException(nameof(inschrijvingsstatus));
            }

            _context.Inschrijvingsstatussen.Remove(inschrijvingsstatus);
        }


        // Updaten
        public void UpdatenInschrijvingsstatus(Inschrijvingsstatus inschrijvingsstatus)
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
            _context.Inschrijvingsstatussen.RemoveRange(GetInschrijvingsstatussen());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Inschrijvingsstatus> items = new List<Inschrijvingsstatus>();
            items.Add(new Inschrijvingsstatus()
            {
                Id = Guid.Parse("4c83bb5b-30b2-4ac7-9662-eab035157b86"),
                Naam = "Aangevraagd",
                Volgorde = 1
            });
            items.Add(new Inschrijvingsstatus()
            {
                Id = Guid.Parse("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"),
                Naam = "Goedgekeurd",
                Volgorde = 2
            });
            items.Add(new Inschrijvingsstatus()
            {
                Id = Guid.Parse("adb494b6-10ae-495a-9ba4-48ef04d0e29f"),
                Naam = "Gepland",
                Volgorde = 3
            });
            items.Add(new Inschrijvingsstatus()
            {
                Id = Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"),
                Naam = "Afgekeurd",
                Volgorde = 4
            });
            _context.Inschrijvingsstatussen.AddRange(items);
            Opslaan();
        }
    }
}
