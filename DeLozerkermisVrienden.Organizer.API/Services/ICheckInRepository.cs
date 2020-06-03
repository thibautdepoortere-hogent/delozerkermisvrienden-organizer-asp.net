using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Entities;
using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public interface ICheckInRepository
    {
        // CheckIn
        bool BestaatCheckIn(Guid checkInId);
        void ToevoegenCheckIn(CheckIn checkIn);
        void UpdatenCheckIn(CheckIn checkIn);
        void VerwijderenCheckIn(CheckIn checkIn);
        void VerwijderAlleCheckInsVanInschrijving(Guid inschrijvingsId);
        CheckIn GetCheckIn(Guid checkInId);
        IEnumerable<CheckIn> GetCheckIns();
        IEnumerable<CheckIn> GetCheckIns(CheckInsResourceParameters checkInsResourceParameters);
        int GetAantalCheckInsVanInschrijving(Guid inschrijvingsId);
        int GetAantalCheckInsVanLid(Guid lidId);
        bool Opslaan();

        void FabrieksInstellingenVerwijderAlles();
        void FabrieksInstellingenTerugzetten();
    }
}
