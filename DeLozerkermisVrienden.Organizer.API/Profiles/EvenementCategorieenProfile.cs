using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class EvenementCategorieenProfile : Profile
    {
        public EvenementCategorieenProfile()
        {
            CreateMap<Entities.EvenementCategorie, Models.EvenementCategorieVoorRaadpleegDto>();
            CreateMap<Entities.EvenementCategorie, Models.EvenementCategorieVoorUpdateDto>();
            CreateMap<Models.EvenementCategorieVoorAanmaakDto, Entities.EvenementCategorie>();
            CreateMap<Models.EvenementCategorieVoorUpdateDto, Entities.EvenementCategorie>();
        }
    }
}
