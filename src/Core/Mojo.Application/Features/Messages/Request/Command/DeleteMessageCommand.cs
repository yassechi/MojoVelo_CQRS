namespace Mojo.Application.Features.Messages.Request.Command
{
    public class DeleteMessageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
