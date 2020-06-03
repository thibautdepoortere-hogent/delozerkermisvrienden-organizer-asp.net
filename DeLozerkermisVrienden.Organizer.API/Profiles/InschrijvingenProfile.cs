using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class InschrijvingenProfile : Profile
    {
        public InschrijvingenProfile()
        {
            CreateMap<Entities.Inschrijving, Models.InschrijvingVoorRaadpleegDto>()
                .ForMember(
                    dest => dest.DatumInschrijving,
                    opt => opt.MapFrom(src => src.DatumInschrijving.DateTimeOmzettenNaarExportNotatie())
                );
            CreateMap<Entities.Inschrijving, Models.InschrijvingVoorRaadpleegStatusDto>()
                .ForMember(
                    dest => dest.DatumInschrijving,
                    opt => opt.MapFrom(src => src.DatumInschrijving.DateTimeOmzettenNaarExportNotatie())
                );
            CreateMap<Entities.Inschrijving, Models.InschrijvingVoorUpdateDto>();
            CreateMap<Models.InschrijvingVoorAanmaakDto, Entities.Inschrijving>();
            CreateMap<Models.InschrijvingVoorUpdateDto, Entities.Inschrijving>();
        }
    }
}
