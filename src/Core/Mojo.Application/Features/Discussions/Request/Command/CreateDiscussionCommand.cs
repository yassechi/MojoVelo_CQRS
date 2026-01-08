namespace Mojo.Application.Features.Discussions.Request.Command
{
    internal class CreateDiscussionCommand : IRequest<Unit>
    {
        public DiscussionDto dto { get; set; }
    }
}
