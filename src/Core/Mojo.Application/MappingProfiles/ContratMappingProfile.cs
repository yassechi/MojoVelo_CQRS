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
