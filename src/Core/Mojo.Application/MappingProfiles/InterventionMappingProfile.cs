using Mojo.Application.DTOs.EntitiesDto.Intervention;

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
