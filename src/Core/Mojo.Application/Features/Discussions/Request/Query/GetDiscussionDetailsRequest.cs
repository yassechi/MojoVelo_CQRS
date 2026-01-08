namespace Mojo.Application.Features.Discussions.Request.Query
{
    internal class GetDiscussionDetailsRequest : IRequest<DiscussionDto>
    {
        public int Id { get; set; }
    }
}
