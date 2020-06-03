using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class BetaaltransactiesProfile : Profile
    {
        public BetaaltransactiesProfile()
        {
            CreateMap<Entities.Betaaltransactie, Models.BetaaltransactieVoorRaadpleegDto>()
                .ForMember(
                    dest => dest.DatumBetaling,
                    opt => opt.MapFrom(src => src.DatumBetaling.DateTimeOmzettenNaarExportNotatie())
                )
                .ForMember(
                    dest => dest.VerantwoordelijkeBetaling,
                    opt => opt.MapFrom(src => $"{src.VerantwoordelijkeBetaling}")
                )
                .ForMember(
                    dest => dest.GestructureerdeMededeling,
                    opt => opt.MapFrom(src => $"{src.GestructureerdeMededeling}")
                )
                ;
            CreateMap<Entities.Betaaltransactie, Models.BetaaltransactieVoorUpdateDto>();
            CreateMap<Models.BetaaltransactieVoorAanmaakDto, Entities.Betaaltransactie>();
            CreateMap<Models.BetaaltransactieVoorUpdateDto, Entities.Betaaltransactie>();
        }
    }
}
