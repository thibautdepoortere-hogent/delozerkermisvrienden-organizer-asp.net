using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class BetaalmethodenProfile : Profile
    {
        public BetaalmethodenProfile()
        {
            CreateMap<Entities.Betaalmethode, Models.BetaalmethodeVoorRaadpleegDto>()
                .ForMember(
                    dest => dest.Opmerking,
                    opt => opt.MapFrom(src => $"{src.Opmerking}")
                );
            CreateMap<Entities.Betaalmethode, Models.BetaalmethodeVoorUpdateDto>();
            CreateMap<Models.BetaalmethodeVoorAanmaakDto, Entities.Betaalmethode>();
            CreateMap<Models.BetaalmethodeVoorUpdateDto, Entities.Betaalmethode>();
        }
    }
}