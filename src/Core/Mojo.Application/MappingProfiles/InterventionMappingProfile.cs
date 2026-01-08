namespace Mojo.Application.MappingProfiles
{
    public class InterventionMappingProfile : Profile
    {
        public InterventionMappingProfile()
        {
            CreateMap<Intervention, InterventionDto>().ReverseMap();

        }
    }
}
