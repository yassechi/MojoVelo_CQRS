namespace Mojo.Application.Features.Messages.Request.Command
{
    public class DeleteMessageCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
