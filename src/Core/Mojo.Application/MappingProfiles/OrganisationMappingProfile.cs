namespace Mojo.Application.MappingProfiles
{
    public class OrganisationMappingProfile : Profile
    {
        public OrganisationMappingProfile()
        {
            CreateMap<Organisation, OrganisationDto>().ReverseMap();

        }
    }
}
