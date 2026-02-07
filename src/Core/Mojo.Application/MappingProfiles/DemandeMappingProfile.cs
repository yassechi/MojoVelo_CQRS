using Mojo.Application.DTOs.EntitiesDto.Amortissement;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.MappingProfiles
{
    public class DemandeMappingProfile : Profile
    {
        public DemandeMappingProfile()
        {
            //Configure Mapping
            CreateMap<Demande, DemandeDto>().ReverseMap();

        }
    }
}
