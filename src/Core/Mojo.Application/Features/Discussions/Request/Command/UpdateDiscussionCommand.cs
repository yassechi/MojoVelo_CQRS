namespace Mojo.Application.Features.Discussions.Request.Command
{
    internal class UpdateDiscussionCommand : IRequest<Unit>
    {
        public DiscussionDto dto { get; set; }
    }
}
