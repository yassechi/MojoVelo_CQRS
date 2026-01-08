namespace Mojo.Application.Features.Messages.Request.Command
{
    internal class DeleteMessageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
