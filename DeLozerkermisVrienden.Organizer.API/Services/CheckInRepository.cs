using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class CheckInRepository : ICheckInRepository, IDisposable
    {
        private readonly OrganizerContext _context;

        public CheckInRepository(OrganizerContext context)
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
        public bool BestaatCheckIn(Guid checkInId)
        {
            if (checkInId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(checkInId));
            }

            return _context.CheckIns.Any(c => c.Id == checkInId);
        }


        // Get (Single)
        public CheckIn GetCheckIn(Guid checkInId)
        {
            if (checkInId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(checkInId));
            }

            return _context.CheckIns.FirstOrDefault(c => c.Id == checkInId);
        }


        // Get (Collection)
        public IEnumerable<CheckIn> GetCheckIns()
        {
            return _context.CheckIns.OrderBy(c => c.CheckInMoment).ToList();
        }

        public IEnumerable<CheckIn> GetCheckIns(CheckInsResourceParameters checkInenResourceParameters)
        {
            if (checkInenResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(checkInenResourceParameters));
            }

            if (!checkInenResourceParameters.Inschrijving.HasValue && !checkInenResourceParameters.Lid.HasValue && !checkInenResourceParameters.CheckInMomentStartPeriode.HasValue && !checkInenResourceParameters.CheckInMomentEindPeriode.HasValue)
            {
                return GetCheckIns();
            }

            if (!checkInenResourceParameters.Inschrijving.HasValue)
            {
                if (checkInenResourceParameters.Inschrijving == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(checkInenResourceParameters.Inschrijving));
                }
            }

            if (!checkInenResourceParameters.Lid.HasValue)
            {
                if (checkInenResourceParameters.Lid == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(checkInenResourceParameters.Lid));
                }
            }

            if (checkInenResourceParameters.CheckInMomentStartPeriode.HasValue && checkInenResourceParameters.CheckInMomentEindPeriode.HasValue)
            {
                if (checkInenResourceParameters.CheckInMomentStartPeriode > checkInenResourceParameters.CheckInMomentEindPeriode)
                {
                    throw new ArgumentException("De opgegeven periode is ongeldig.");
                }
            }

            var collectie = _context.CheckIns as IQueryable<CheckIn>;

            if (checkInenResourceParameters.Inschrijving.HasValue)
            {
                collectie = collectie.Where(c => c.InschrijvingsId == checkInenResourceParameters.Inschrijving);
            }

            if (checkInenResourceParameters.Lid.HasValue)
            {
                collectie = collectie.Where(c => c.LidId == checkInenResourceParameters.Lid);
            }

            if (checkInenResourceParameters.CheckInMomentStartPeriode.HasValue)
            {
                collectie = collectie.Where(c => c.CheckInMoment >= checkInenResourceParameters.CheckInMomentStartPeriode);
            }

            if (checkInenResourceParameters.CheckInMomentEindPeriode.HasValue)
            {
                collectie = collectie.Where(c => c.CheckInMoment <= checkInenResourceParameters.CheckInMomentEindPeriode);
            }

            return collectie.OrderBy(c => c.CheckInMoment).ToList();
        }


        // Get (Extra)
        public int GetAantalCheckInsVanInschrijving(Guid inschrijvingsId)
        {
            return _context.CheckIns.Count(c => c.InschrijvingsId == inschrijvingsId);
        }

        public int GetAantalCheckInsVanLid(Guid lidId)
        {
            return _context.CheckIns.Count(c => c.LidId == lidId);
        }


        // Toevoegen
        public void ToevoegenCheckIn(CheckIn checkIn)
        {
            if (checkIn == null)
            {
                throw new ArgumentNullException(nameof(checkIn));
            }

            if (checkIn.Id == Guid.Empty)
            {
                checkIn.Id = Guid.NewGuid();
            }

            _context.CheckIns.Add(checkIn);
        }


        // Verwijderen
        public void VerwijderenCheckIn(CheckIn checkIn)
        {
            if (checkIn == null)
            {
                throw new ArgumentNullException(nameof(checkIn));
            }

            _context.CheckIns.Remove(checkIn);
        }

        public void VerwijderAlleCheckInsVanInschrijving(Guid inschrijvingsId)
        {
            if (inschrijvingsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsId));
            }

            IEnumerable<CheckIn> teVerwijderenCheckIns = _context.CheckIns.Where(c => c.InschrijvingsId == inschrijvingsId);
            foreach (CheckIn checkIn in teVerwijderenCheckIns)
            {
                VerwijderenCheckIn(checkIn);
            }
        }


        // Updaten
        public void UpdatenCheckIn(CheckIn checkIn)
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
            _context.CheckIns.RemoveRange(GetCheckIns());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<CheckIn> items = new List<CheckIn>();
            _context.CheckIns.AddRange(items);
            Opslaan();
        }
    }
}
