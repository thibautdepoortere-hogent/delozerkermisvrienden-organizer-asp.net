using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class NieuwsbriefRepository : INieuwsbriefRepository
    {
        private readonly OrganizerContext _context;

        public NieuwsbriefRepository(OrganizerContext context)
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
        public bool BestaatNieuwsbrief(Guid nieuwsbriefId) {
            if (nieuwsbriefId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(nieuwsbriefId));
            }

            return _context.Nieuwsbrieven.Any(n => n.Id == nieuwsbriefId);
        }

        public bool BestaatNieuwsbrief(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return _context.Nieuwsbrieven.Any(n => n.Email.ToUpper() == email.ToUpper());
        }


        // Get (Collection)
        public IEnumerable<Nieuwsbrief> GetNieuwsbrieven()
        {
            return _context.Nieuwsbrieven.OrderBy(n => n.Email).ToList();
        }


        // Toevoegen
        public void ToevoegenNieuwsbrief(Nieuwsbrief nieuwsbrief)
        {
            if (nieuwsbrief == null)
            {
                throw new ArgumentNullException(nameof(nieuwsbrief));
            }

            if (nieuwsbrief.Id == Guid.Empty)
            {
                nieuwsbrief.Id = Guid.NewGuid();
            }

            _context.Nieuwsbrieven.Add(nieuwsbrief);
        }


        // Opslaan
        public bool Opslaan()
        {
            return (_context.SaveChanges() >= 0);
        }


        // Fabrieksinstellingen terugzetten
        public void FabrieksInstellingenVerwijderAlles()
        {
            _context.Nieuwsbrieven.RemoveRange(GetNieuwsbrieven());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Nieuwsbrief> items = new List<Nieuwsbrief>();
            _context.Nieuwsbrieven.AddRange(items);
            Opslaan();
        }
    }
}
