using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class LidRepository : ILidRepository, IDisposable
    {
        private readonly OrganizerContext _context;

        public LidRepository(OrganizerContext context)
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
        public bool BestaatLid(Guid lidId)
        {
            if (lidId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lidId));
            }

            return _context.Leden.Any(l => l.Id == lidId);
        }

        public bool BestaatLid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return _context.Leden.Any(l => l.Email.ToUpper() == email.ToUpper());
        }

        public bool BestaatLidMetUitzonderingVan(string email, Guid lidId)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (lidId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lidId));
            }

            return _context.Leden.Where(l => l.Id != lidId).Any(l => l.Email.ToUpper() == email.ToUpper());
        }


        // Get (Single)
        public Lid GetLid(Guid lidId)
        {
            if (lidId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lidId));
            }

            return _context.Leden.FirstOrDefault(l => l.Id == lidId);
        }

        public Lid GetLid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return _context.Leden.FirstOrDefault(l => l.Email.ToUpper() == email.ToUpper());
        }


        // Get (Collection)
        public IEnumerable<Lid> GetLeden()
        {
            return _context.Leden.OrderBy(l => l.Achternaam).ToList();
        }

        public IEnumerable<Lid> GetLeden(LedenResourceParameters ledenResourceParameters)
        {
            if (ledenResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(ledenResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(ledenResourceParameters.Voornaam) && string.IsNullOrWhiteSpace(ledenResourceParameters.Achternaam) && string.IsNullOrWhiteSpace(ledenResourceParameters.Email) && !ledenResourceParameters.Actief.HasValue)
            {
                return GetLeden();
            }

            var collectie = _context.Leden as IQueryable<Lid>;

            if (!string.IsNullOrWhiteSpace(ledenResourceParameters.Voornaam))
            {
                collectie = collectie.Where(l => l.Voornaam.ToUpper().Contains(ledenResourceParameters.Voornaam.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(ledenResourceParameters.Achternaam))
            {
                collectie = collectie.Where(l => l.Achternaam.ToUpper().Contains(ledenResourceParameters.Achternaam.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(ledenResourceParameters.Email))
            {
                collectie = collectie.Where(l => l.Email.ToUpper().Contains(ledenResourceParameters.Email.ToUpper()));
            }

            if (ledenResourceParameters.Actief.HasValue)
            {
                collectie = collectie.Where(l => l.Actief == ledenResourceParameters.Actief.Value);
            }

            return collectie.OrderBy(l => l.Achternaam).ToList();
        }


        // Toevoegen
        public void ToevoegenLid(Lid lid)
        {
            if (lid == null)
            {
                throw new ArgumentNullException(nameof(lid));
            }

            if (lid.Id == Guid.Empty)
            {
                lid.Id = Guid.NewGuid();
            }

            _context.Leden.Add(lid);
        }


        // Verwijderen
        public void VerwijderenLid(Lid lid)
        {
            if (lid == null)
            {
                throw new ArgumentNullException(nameof(lid));
            }

            _context.Leden.Remove(lid);
        }


        // Updaten
        public void UpdatenLid(Lid lid)
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
            _context.Leden.RemoveRange(GetLeden());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Lid> items = new List<Lid>();
            items.Add(new Lid()
            {
                Id = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2"),
                Voornaam = "Thibaut",
                Achternaam = "De Poortere",
                Email = "thibaut.depoortere@student.hogent.be",
                Actief = true
            });
            items.Add(new Lid()
            {
                Id = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
                Voornaam = "Filip",
                Achternaam = "De Poortere",
                Email = "verzonnenmail01@gmail.com",
                Actief = true
            });
            _context.Leden.AddRange(items);
            Opslaan();
        }
    }
}
