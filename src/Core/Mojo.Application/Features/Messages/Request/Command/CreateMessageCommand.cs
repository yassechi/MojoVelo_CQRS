using Mojo.Application.DTOs.EntitiesDto.Message;

namespace Mojo.Application.Features.Messages.Request.Command
{
    public class CreateMessageCommand :  IRequest<BaseResponse>
    {
        public MessageDto dto { get; set; }
    }
}
