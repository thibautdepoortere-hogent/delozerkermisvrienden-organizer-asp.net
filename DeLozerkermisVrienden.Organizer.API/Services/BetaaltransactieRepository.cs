using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class BetaaltransactieRepository : IBetaaltransactieRepository, IDisposable
    {
        private readonly OrganizerContext _context;

        public BetaaltransactieRepository(OrganizerContext context)
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
        public bool BestaatBetaaltransactie(Guid betaaltransactieId)
        {
            if (betaaltransactieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(betaaltransactieId));
            }

            return _context.Betaaltransacties.Any(b => b.Id == betaaltransactieId);
        }


        // Get (Single)
        public Betaaltransactie GetBetaaltransactie(Guid betaaltransactieId)
        {
            if (betaaltransactieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(betaaltransactieId));
            }

            return _context.Betaaltransacties.FirstOrDefault(b => b.Id == betaaltransactieId);
        }


        // Get (Collection)
        public IEnumerable<Betaaltransactie> GetBetaaltransacties()
        {
            return _context.Betaaltransacties.OrderBy(b => b.DatumBetaling).ToList();
        }

        public IEnumerable<Betaaltransactie> GetBetaaltransacties(BetaaltransactiesResourceParameters betaaltransactiesResourceParameters)
        {
            var collectie = _context.Betaaltransacties as IQueryable<Betaaltransactie>;

            if (betaaltransactiesResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(betaaltransactiesResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(betaaltransactiesResourceParameters.VerantwoordelijkeBetaling) && string.IsNullOrWhiteSpace(betaaltransactiesResourceParameters.GestructureerdeMededeling) && !betaaltransactiesResourceParameters.Inschrijving.HasValue && !betaaltransactiesResourceParameters.Betaalmethode.HasValue && !betaaltransactiesResourceParameters.Lid.HasValue)
            {
                return GetBetaaltransacties();
            }

            if (!string.IsNullOrWhiteSpace(betaaltransactiesResourceParameters.VerantwoordelijkeBetaling))
            {
                collectie = collectie.Where(b => b.VerantwoordelijkeBetaling.ToUpper().Contains(betaaltransactiesResourceParameters.VerantwoordelijkeBetaling.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(betaaltransactiesResourceParameters.GestructureerdeMededeling))
            {
                collectie = collectie.Where(b => b.GestructureerdeMededeling.ToUpper() == betaaltransactiesResourceParameters.GestructureerdeMededeling.ToUpper());
            }

            if (betaaltransactiesResourceParameters.Inschrijving.HasValue)
            {
                collectie = collectie.Where(b => b.InschrijvingsId == betaaltransactiesResourceParameters.Inschrijving.Value);
            }

            if (betaaltransactiesResourceParameters.Betaalmethode.HasValue)
            {
                collectie = collectie.Where(b => b.BetaalmethodeId == betaaltransactiesResourceParameters.Betaalmethode.Value);
            }

            if (betaaltransactiesResourceParameters.Lid.HasValue)
            {
                collectie = collectie.Where(b => b.LidId == betaaltransactiesResourceParameters.Lid.Value);
            }

            return collectie.OrderBy(b => b.DatumBetaling).ToList();
        }


        // Get (extra)
        public int GetAantalBetaaltransactiesVanInschrijving(Guid inschrijvingsId)
        {
            return _context.Betaaltransacties.Count(b => b.InschrijvingsId == inschrijvingsId);
        }

        public int GetAantalBetaaltransactiesVanBetaalmethode(Guid betaalmethodeId)
        {
            return _context.Betaaltransacties.Count(b => b.BetaalmethodeId == betaalmethodeId);
        }

        public int GetAantalBetaaltransactiesVanLid(Guid lidId)
        {
            return _context.Betaaltransacties.Count(b => b.LidId == lidId);
        }



        // Toevoegen
        public void ToevoegenBetaaltransactie(Betaaltransactie betaaltransactie)
        {
            if (betaaltransactie == null)
            {
                throw new ArgumentNullException(nameof(betaaltransactie));
            }

            if (betaaltransactie.Id == Guid.Empty)
            {
                betaaltransactie.Id = Guid.NewGuid();
            }
            _context.Betaaltransacties.Add(betaaltransactie);
        }


        // Verwijderen
        public void VerwijderenBetaaltransactie(Betaaltransactie betaaltransactie)
        {
            if (betaaltransactie == null)
            {
                throw new ArgumentNullException(nameof(betaaltransactie));
            }

            _context.Betaaltransacties.Remove(betaaltransactie);
        }

        public void VerwijderAlleBetaaltransactiesVanInschrijving(Guid inschrijvingsId)
        {
            if (inschrijvingsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsId));
            }

            IEnumerable<Betaaltransactie> teVerwijderenBetaaltransacties = _context.Betaaltransacties.Where(b => b.InschrijvingsId == inschrijvingsId);
            foreach (Betaaltransactie betaaltransactie in teVerwijderenBetaaltransacties)
            {
                VerwijderenBetaaltransactie(betaaltransactie);
            }
        }


        // Updaten
        public void UpdatenBetaaltransactie(Betaaltransactie betaaltransactie)
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
            _context.Betaaltransacties.RemoveRange(GetBetaaltransacties());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Betaaltransactie> items = new List<Betaaltransactie>();
            _context.Betaaltransacties.AddRange(items);
            Opslaan();
        }
    }
}
