using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Profiles
{
    public class NieuwsbrievenProfile : Profile
    {
        public NieuwsbrievenProfile()
        {
            CreateMap<Models.NieuwsbriefVoorAanmaakDto, Entities.Nieuwsbrief>();
        }
    }
}
