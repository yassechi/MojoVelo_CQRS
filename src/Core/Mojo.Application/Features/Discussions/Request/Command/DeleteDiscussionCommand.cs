namespace Mojo.Application.Features.Discussions.Request.Command
{
    public class DeleteDiscussionCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
