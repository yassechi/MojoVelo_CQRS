namespace Mojo.Application.Features.VuesMessages.Request.Command
{
    public class MarkMessagesReadCommand : IRequest<BaseResponse>
    {
        public string UserId { get; set; } = string.Empty;
        public int? DiscussionId { get; set; }
        public List<int> MessageIds { get; set; } = new();
    }
}
