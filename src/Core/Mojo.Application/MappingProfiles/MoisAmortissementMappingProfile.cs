using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;

namespace Mojo.Application.MappingProfiles
{
    public class MoisAmortissementMappingProfile : Profile
    {
        public MoisAmortissementMappingProfile()
        {
            CreateMap<MoisAmortissement, MoisAmortissementDto>().ReverseMap();
        }
    }
}
