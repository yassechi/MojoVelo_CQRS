using Mojo.Application.DTOs.EntitiesDto.Discussion;

namespace Mojo.Application.Features.Discussions.Request.Command
{
    public class CreateDiscussionCommand : IRequest<BaseResponse>
    {
        public DiscussionDto dto { get; set; }
    }
}
