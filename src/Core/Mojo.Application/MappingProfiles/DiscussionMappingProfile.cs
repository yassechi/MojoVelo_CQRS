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
