using Mojo.Application.DTOs.EntitiesDto.Discussion;

namespace Mojo.Application.Features.Discussions.Request.Query
{
    public class GetDiscussionDetailsRequest : IRequest<DiscussionDto>
    {
        public int Id { get; set; }
    }
}
