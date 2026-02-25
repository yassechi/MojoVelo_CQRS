using Mojo.Application.Features.VuesMessages.Request.Command;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.VuesMessages.Handler.Command
{
    public class MarkMessagesReadHandler : IRequestHandler<MarkMessagesReadCommand, BaseResponse>
    {
        private readonly IVuesMessageRepository _vuesMessageRepository;
        private readonly IMessageRepository _messageRepository;

        public MarkMessagesReadHandler(
            IVuesMessageRepository vuesMessageRepository,
            IMessageRepository messageRepository)
        {
            _vuesMessageRepository = vuesMessageRepository;
            _messageRepository = messageRepository;
        }

        public async Task<BaseResponse> Handle(MarkMessagesReadCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                response.Success = false;
                response.Message = "UserId requis.";
                return response;
            }

            var messageIds = new HashSet<int>(request.MessageIds.Where(id => id > 0));

            if (request.DiscussionId.HasValue && request.DiscussionId.Value > 0)
            {
                var messages = await _messageRepository.GetByDiscussionId(request.DiscussionId.Value);
                foreach (var message in messages)
                {
                    messageIds.Add(message.Id);
                }
            }

            if (messageIds.Count == 0)
            {
                response.Success = false;
                response.Message = "Aucun message à marquer comme lu.";
                return response;
            }

            var updated = await _vuesMessageRepository.MarkMessagesAsReadAsync(request.UserId, messageIds);

            response.Success = true;
            response.Message = "Messages marqués comme lus.";
            response.Id = updated;
            return response;
        }
    }
}
