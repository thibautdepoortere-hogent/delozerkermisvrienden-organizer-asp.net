using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class InschrijvingRepository : IInschrijvingRepository, IDisposable
    {
        private readonly OrganizerContext _context;
        private readonly IBetaaltransactieRepository _betaaltransactieRepository;
        private readonly ICheckInRepository _checkInRepository;

        public InschrijvingRepository(OrganizerContext context, IBetaaltransactieRepository betaaltransactie, ICheckInRepository checkInRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _betaaltransactieRepository = betaaltransactie ?? throw new ArgumentNullException(nameof(betaaltransactie));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(checkInRepository));
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
        public bool BestaatInschrijving(Guid inschrijvingsId)
        {
            if (inschrijvingsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsId));
            }

            return _context.Inschrijvingen.Any(i => i.Id == inschrijvingsId);
        }


        // Get (Single)
        public Inschrijving GetInschrijving(Guid inschrijvingsId)
        {
            if (inschrijvingsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inschrijvingsId));
            }

            return _context.Inschrijvingen.FirstOrDefault(i => i.Id == inschrijvingsId);
        }


        // Get (Collection)
        public IEnumerable<Inschrijving> GetInschrijvingen()
        {
            IQueryable<Inschrijving> collectie = _context.Inschrijvingen;
            return GetGesorteerdeInschrijvingen(null, collectie).ToList();
        }

        public IEnumerable<Inschrijving> GetInschrijvingen(InschrijvingenResourceParameters inschrijvingenResourceParameters)
        {
            var collectie = _context.Inschrijvingen as IQueryable<Inschrijving>;

            if (inschrijvingenResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(inschrijvingenResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Id) && string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Voornaam) && string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Achternaam) && string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Standnummer) && string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.GestructureerdeMededeling) && string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.QRCode) && !inschrijvingenResourceParameters.NogNietIngecheckt.HasValue && !inschrijvingenResourceParameters.Evenement.HasValue && !inschrijvingenResourceParameters.Inschrijvingsstatus.HasValue)
            {
                if(!inschrijvingenResourceParameters.SorteerMethode.HasValue)
                {
                    return GetInschrijvingen();
                }
                else
                {
                    return GetGesorteerdeInschrijvingen(inschrijvingenResourceParameters.SorteerMethode, collectie).ToList();
                }
            }

            if (!string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Id))
            {
                collectie = collectie.Where(i => i.Id.ToString().ToUpper().Contains(inschrijvingenResourceParameters.Id.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Voornaam))
            {
                collectie = collectie.Where(i => i.Voornaam.ToUpper().Contains(inschrijvingenResourceParameters.Voornaam.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Achternaam))
            {
                collectie = collectie.Where(i => i.Achternaam.ToUpper().Contains(inschrijvingenResourceParameters.Achternaam.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.Standnummer))
            {
                collectie = collectie.Where(i => i.Standnummer.ToUpper().Contains(inschrijvingenResourceParameters.Standnummer.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.GestructureerdeMededeling))
            {
                collectie = collectie.Where(i => i.GestructureerdeMededeling.ToUpper() == inschrijvingenResourceParameters.GestructureerdeMededeling.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(inschrijvingenResourceParameters.QRCode))
            {
                collectie = collectie.Where(i => i.QRCode.ToUpper() == inschrijvingenResourceParameters.QRCode.ToUpper());
            }

            if (inschrijvingenResourceParameters.Evenement.HasValue)
            {
                collectie = collectie.Where(i => i.EvenementId == inschrijvingenResourceParameters.Evenement);
            }

            if (inschrijvingenResourceParameters.Inschrijvingsstatus.HasValue)
            {
                collectie = collectie.Where(i => i.InschrijvingsstatusId == inschrijvingenResourceParameters.Inschrijvingsstatus);
            }

            if (inschrijvingenResourceParameters.NogNietIngecheckt.HasValue && inschrijvingenResourceParameters.NogNietIngecheckt.Value)
            {
                var collectieAlsList = collectie.ToList();
                var nieuweCollectie = new List<Inschrijving>();
                for (int i = 0; i < collectieAlsList.Count(); i++)
                {
                    if(_checkInRepository.GetAantalCheckInsVanInschrijving(collectieAlsList[i].Id) == 0)
                    {
                        nieuweCollectie.Add(collectieAlsList[i]);
                    }
                }
                collectie = nieuweCollectie.AsQueryable() as IQueryable<Inschrijving>;
            }

            collectie = GetGesorteerdeInschrijvingen(inschrijvingenResourceParameters.SorteerMethode, collectie);

            return collectie.ToList();
        }

        private IQueryable<Inschrijving> GetGesorteerdeInschrijvingen(Sorteer.SorteerMethode? sorteermethode, IQueryable<Inschrijving> collectie)
        {
            if (sorteermethode == null)
            {
                sorteermethode = Sorteer.SorteerMethode.DatumInschrijving;
            }

            switch (sorteermethode)
            {
                case Sorteer.SorteerMethode.DatumInschrijving:
                default:
                    return collectie.OrderBy(i => i.DatumInschrijving).ThenByDescending(i => i.AantalMeter);
                case Sorteer.SorteerMethode.AantalMeter:
                    return collectie.OrderByDescending(i => i.AantalMeter).ThenBy(i => i.DatumInschrijving);
            }
        }


        // Get (extra)
        public int GetAantalInschrijvingenVanBetaalmethode(Guid betaalmethodeId)
        {
            return _context.Inschrijvingen.Count(i => i.BetaalmethodeId == betaalmethodeId);
        }

        public int GetAantalInschrijvingenVanInschrijvingsstatus(Guid inschrijvingsstatusId)
        {
            return _context.Inschrijvingen.Count(i => i.InschrijvingsstatusId == inschrijvingsstatusId);
        }

        public int GetAantalInschrijvingenVanEvenement(Guid evenementId)
        {
            return _context.Inschrijvingen.Count(i => i.EvenementId == evenementId);
        }

        public int GetAantalInschrijvingenVanLid(Guid lidId)
        {
            return _context.Inschrijvingen.Count(i => i.LidId == lidId);
        }



        // Toevoegen
        public void ToevoegenInschrijving(Inschrijving inschrijving)
        {
            if (inschrijving == null)
            {
                throw new ArgumentNullException(nameof(inschrijving));
            }

            if(inschrijving.Id == Guid.Empty)
            {
                inschrijving.Id = Guid.NewGuid();
            }

            _context.Inschrijvingen.Add(inschrijving);
        }


        // Verwijderen
        public void VerwijderenInschrijving(Inschrijving inschrijving)
        {
            if (inschrijving == null)
            {
                throw new ArgumentNullException(nameof(inschrijving));
            }

            if (_betaaltransactieRepository.GetAantalBetaaltransactiesVanInschrijving(inschrijving.Id) > 0)
            {
                _betaaltransactieRepository.VerwijderAlleBetaaltransactiesVanInschrijving(inschrijving.Id);
            }

            if (_checkInRepository.GetAantalCheckInsVanInschrijving(inschrijving.Id) > 0)
            {
                _checkInRepository.VerwijderAlleCheckInsVanInschrijving(inschrijving.Id);
            }

            _context.Inschrijvingen.Remove(inschrijving);
        }

        public void VerwijderAlleInschrijvingenVanEvenement(Guid evenementId)
        {
            if (evenementId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(evenementId));
            }

            IEnumerable<Inschrijving> teVerwijderenInschrijvingen = _context.Inschrijvingen.Where(i => i.EvenementId == evenementId);
            foreach (Inschrijving inschrijving in teVerwijderenInschrijvingen)
            {
                VerwijderenInschrijving(inschrijving);
            }
        }


        // Updaten
        public void UpdatenInschrijving(Inschrijving inschrijving)
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
            _context.Inschrijvingen.RemoveRange(GetInschrijvingen());
            Opslaan();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Inschrijving> items = new List<Inschrijving>();
            _context.Inschrijvingen.AddRange(items);
            Opslaan();
        }
    }
}
