namespace Mojo.Application.Features.Discussions.Request.Command
{
    internal class DeleteDiscussionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
