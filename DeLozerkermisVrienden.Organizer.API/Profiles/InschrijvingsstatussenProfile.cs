using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class InschrijvingsstatussenProfile : Profile
    {
        public InschrijvingsstatussenProfile()
        {
            CreateMap<Entities.Inschrijvingsstatus, Models.InschrijvingsstatusVoorRaadpleegDto>();
            CreateMap<Entities.Inschrijvingsstatus, Models.InschrijvingsstatusVoorUpdateDto>();
            CreateMap<Models.InschrijvingsstatusVoorAanmaakDto, Entities.Inschrijvingsstatus>();
            CreateMap<Models.InschrijvingsstatusVoorUpdateDto, Entities.Inschrijvingsstatus>();
        }
    }
}
