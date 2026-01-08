namespace Mojo.Application.MappingProfiles
{
    public class AmortissementMappingProfile : Profile
    {
        public AmortissementMappingProfile()
        {
            //Configure Mapping
            CreateMap<Amortissement, AmortissmentDto>().ReverseMap();
            
        }
    }
}
