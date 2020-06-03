using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class LedenProfile : Profile
    {
        public LedenProfile()
        {
            CreateMap<Entities.Lid, Models.LidVoorRaadpleegDto>();
            CreateMap<Entities.Lid, Models.LidVoorUpdateDto>();
            CreateMap<Models.LidVoorAanmaakDto, Entities.Lid>();
            CreateMap<Models.LidVoorUpdateDto, Entities.Lid>();
        }
    }
}
