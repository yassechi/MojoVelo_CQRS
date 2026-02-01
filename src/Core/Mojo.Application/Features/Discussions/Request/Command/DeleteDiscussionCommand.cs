namespace Mojo.Application.Features.Discussions.Request.Command
{
    public class DeleteDiscussionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
