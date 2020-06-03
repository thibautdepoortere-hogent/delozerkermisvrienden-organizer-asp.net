using DeLozerkermisVrienden.Organizer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface ILoginRepository
    {
        bool BestaatLogin(Guid lidId);
        Login GetLogin(Guid lidId);

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
