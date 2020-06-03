using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class EvenementenProfile : Profile
    {
        public EvenementenProfile()
        {
            CreateMap<Entities.Evenement, Models.EvenementVoorRaadpleegDto>()
                .ForMember(
                    dest => dest.DatumStartEvenement,
                    opt => opt.MapFrom(src => src.DatumStartEvenement.DateTimeOmzettenNaarExportNotatie())
                )
                .ForMember(
                    dest => dest.DatumEindeEvenement,
                    opt => opt.MapFrom(src => src.DatumEindeEvenement.DateTimeOmzettenNaarExportNotatie())
                )
                .ForMember(
                    dest => dest.DatumStartInschrijvingen,
                    opt => opt.MapFrom(src => src.DatumStartInschrijvingen.DateTimeOmzettenNaarExportNotatie())
                )
                .ForMember(
                    dest => dest.DatumEindeInschrijvingen,
                    opt => opt.MapFrom(src => src.DatumEindeInschrijvingen.DateTimeOmzettenNaarExportNotatie())
                )
                ;
            CreateMap<Entities.Evenement, Models.EvenementVoorUpdateDto>();
            CreateMap<Models.EvenementVoorAanmaakDto, Entities.Evenement>();
            CreateMap<Models.EvenementVoorUpdateDto, Entities.Evenement>();
        }
    }
}
