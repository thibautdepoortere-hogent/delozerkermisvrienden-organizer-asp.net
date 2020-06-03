using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface ILidRepository
    {
        // Lid
        bool BestaatLid(Guid lidId);
        bool BestaatLid(string email);
        bool BestaatLidMetUitzonderingVan(string email, Guid lidId);
        void ToevoegenLid(Lid lid);
        void UpdatenLid(Lid lid);
        void VerwijderenLid(Lid lid);
        Lid GetLid(Guid lidId);
        Lid GetLid(string email);
        IEnumerable<Lid> GetLeden();
        IEnumerable<Lid> GetLeden(LedenResourceParameters ledenResourceParameters);
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
