namespace Mojo.Application.Features.Messages.Request.Command
{
    internal class UpdateMessageCommand : IRequest<Unit>
    {
        public MessageDto dto { get; set; }
    }
}
