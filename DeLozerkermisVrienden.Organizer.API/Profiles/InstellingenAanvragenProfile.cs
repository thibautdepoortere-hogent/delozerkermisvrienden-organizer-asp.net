using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class InstellingenAanvragenProfile : Profile
    {
        public InstellingenAanvragenProfile()
        {
            CreateMap<Entities.InstellingenAanvraag, Models.InstellingenAanvraagVoorRaadpleegDto>();
        }
    }
}
