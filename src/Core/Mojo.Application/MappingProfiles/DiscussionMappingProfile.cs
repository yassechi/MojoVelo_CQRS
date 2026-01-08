using Mojo.Application.DTOs.EntitiesDto.Discussion;

namespace Mojo.Application.MappingProfiles
{
    public class DiscussionMappingProfile : Profile
    {
        public DiscussionMappingProfile()
        {
            CreateMap<Discussion, DiscussionDto>().ReverseMap();

        }
    }
}
