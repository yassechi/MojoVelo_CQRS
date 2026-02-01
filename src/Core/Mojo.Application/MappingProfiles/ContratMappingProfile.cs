using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.MappingProfiles
{
    public class ContratMappingProfile : Profile
    {
        public ContratMappingProfile()
        {
            CreateMap<Contrat, ContratDto>().ReverseMap();

        }
    }
}
