using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;

namespace Mojo.Application.MappingProfiles
{
    public class OrganisationLogoMappingProfile : Profile
    {
        public OrganisationLogoMappingProfile()
        {
            CreateMap<OrganisationLogo, OrganisationLogoDto>().ReverseMap();
        }
    }
}
