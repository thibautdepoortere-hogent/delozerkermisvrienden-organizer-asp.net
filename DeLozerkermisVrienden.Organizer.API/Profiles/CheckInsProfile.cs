using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class CheckInsProfile : Profile
    {
        public CheckInsProfile()
        {
            CreateMap<Entities.CheckIn, Models.CheckInVoorRaadpleegDto>()
                .ForMember(
                    dest => dest.CheckInMoment,
                    opt => opt.MapFrom(src => src.CheckInMoment.DateTimeOmzettenNaarExportNotatie())
                )
                ;
            CreateMap<Entities.CheckIn, Models.CheckInVoorUpdateDto>();
            CreateMap<Models.CheckInVoorAanmaakDto, Entities.CheckIn>();
            CreateMap<Models.CheckInVoorUpdateDto, Entities.CheckIn>();
        }
    }
}
