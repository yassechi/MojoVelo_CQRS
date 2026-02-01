using Mojo.Application.DTOs.EntitiesDto.Message;

namespace Mojo.Application.Features.Messages.Request.Query
{
    public class GetMessageDetailsRequest : IRequest<MessageDto>
    {
        public int Id { get; set; }
    }
}
