using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class LoginRepository : ILoginRepository, IDisposable
    {
        private readonly OrganizerContext _context;

        public LoginRepository(OrganizerContext context)
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
        public bool BestaatLogin(Guid lidId)
        {
            if (lidId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lidId));
            }

            return _context.Logins.Any(l => l.LidId == lidId);
        }


        // Get (Single)
        public Login GetLogin(Guid lidId)
        {
            if (lidId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lidId));
            }

            return _context.Logins.FirstOrDefault(l => l.LidId == lidId);
        }


        // Fabrieksinstellingen terugzetten
        public void FabrieksInstellingenVerwijderAlles()
        {
            _context.Logins.RemoveRange(_context.Logins.ToList());
            _context.SaveChanges();
        }
        public void FabrieksInstellingenTerugzetten()
        {
            ICollection<Login> items = new List<Login>();
            items.Add(new Login()
            {
                LidId = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2"),
                Wachtwoord = "$2x$10$V11TbtztH5K4bi.cRHmqAOwfblupm/nyRgKwC6m9Sr4do1mtYpnmm" // < Hash asp.net core     //Hash react: $2a$12$HB6ha21ETrzefxodR5H.t.dGWLyh/f/E49PUS80zyZ1z2OokPzAC2     //Origineel: Administrator
            });
            items.Add(new Login()
            {
                LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
                Wachtwoord = "$2x$10$V11TbtztH5K4bi.cRHmqAOD5oVfiZROjHGpHvKttRxcfWjnIwPmTa" // < Hash asp.net core     //Hash react: $2a$12$HB6ha21ETrzefxodR5H.t.99EdAbtc1bfbsyvNxWkKYUtgjA6xdoG     //Origineel: Moderator
            });
            _context.Logins.AddRange(items);
            _context.SaveChanges();
        }
    }
}
