using DeLozerkermisVrienden.Organizer.API.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class FabrieksInstellingRepository : IFabrieksInstellingRepository, IDisposable
    {
        private readonly OrganizerContext _context;
        private readonly IBetaalmethodeRepository _betaalmethodeRepository;
        private readonly IBetaaltransactieRepository _betaaltransactieRepository;
        private readonly ICheckInRepository _checkInRepository;
        private readonly IEvenementCategorieRepository _evenementCategorieRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly IInschrijvingRepository _inschrijvingRepository;
        private readonly IInschrijvingsstatusRepository _inschrijvingsstatusRepository;
        private readonly ILidRepository _lidRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly INieuwsbriefRepository _nieuwsbriefRepository;

        public FabrieksInstellingRepository(OrganizerContext context, IBetaalmethodeRepository betaalmethodeRepository, IBetaaltransactieRepository betaaltransactieRepository, ICheckInRepository checkInRepository, IEvenementCategorieRepository evenementCategorieRepository, IEvenementRepository evenementRepository, IInschrijvingRepository inschrijvingRepository, IInschrijvingsstatusRepository inschrijvingsstatusRepository, ILidRepository lidRepository, ILoginRepository loginRepository, INieuwsbriefRepository nieuwsbriefRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _betaalmethodeRepository = betaalmethodeRepository ?? throw new ArgumentNullException(nameof(betaalmethodeRepository));
            _betaaltransactieRepository = betaaltransactieRepository ?? throw new ArgumentNullException(nameof(betaaltransactieRepository));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(checkInRepository));
            _evenementCategorieRepository = evenementCategorieRepository ?? throw new ArgumentNullException(nameof(evenementCategorieRepository));
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
            _inschrijvingRepository = inschrijvingRepository ?? throw new ArgumentNullException(nameof(inschrijvingRepository));
            _inschrijvingsstatusRepository = inschrijvingsstatusRepository ?? throw new ArgumentNullException(nameof(inschrijvingsstatusRepository));
            _lidRepository = lidRepository ?? throw new ArgumentNullException(nameof(lidRepository));
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _nieuwsbriefRepository = nieuwsbriefRepository ?? throw new ArgumentNullException(nameof(nieuwsbriefRepository));

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


        // Fabrieksinstellingen terugzetten
        public void FabrieksInstellingenTerugzetten()
        {
            _checkInRepository.FabrieksInstellingenVerwijderAlles();
            _betaaltransactieRepository.FabrieksInstellingenVerwijderAlles();
            _inschrijvingRepository.FabrieksInstellingenVerwijderAlles();
            _nieuwsbriefRepository.FabrieksInstellingenVerwijderAlles();
            _evenementRepository.FabrieksInstellingenVerwijderAlles();
            _loginRepository.FabrieksInstellingenVerwijderAlles();
            _betaalmethodeRepository.FabrieksInstellingenVerwijderAlles();
            _inschrijvingsstatusRepository.FabrieksInstellingenVerwijderAlles();
            _evenementCategorieRepository.FabrieksInstellingenVerwijderAlles();
            _lidRepository.FabrieksInstellingenVerwijderAlles();

            _lidRepository.FabrieksInstellingenTerugzetten();
            _evenementCategorieRepository.FabrieksInstellingenTerugzetten();
            _inschrijvingsstatusRepository.FabrieksInstellingenTerugzetten();
            _betaalmethodeRepository.FabrieksInstellingenTerugzetten();
            _loginRepository.FabrieksInstellingenTerugzetten();
            _evenementRepository.FabrieksInstellingenTerugzetten();
            _nieuwsbriefRepository.FabrieksInstellingenTerugzetten();
            _inschrijvingRepository.FabrieksInstellingenTerugzetten();
            _betaaltransactieRepository.FabrieksInstellingenTerugzetten();
            _checkInRepository.FabrieksInstellingenTerugzetten();
        }
    }
}
