namespace Mojo.Application.MappingProfiles
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();

        }
    }
}
