using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class LoginsProfile : Profile
    {
        public LoginsProfile()
        {
            CreateMap<Entities.Login, Models.LoginVersleuteldWachtwoordVoorRaadpleegDto>();
        }
    }
}
