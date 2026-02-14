using Mojo.Application.DTOs.EntitiesDto.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Messages.Handler.Query
{
    public class GetMessagesByDiscussionHandler
      : IRequestHandler<GetMessagesByDiscussionRequest, List<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessagesByDiscussionHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> Handle(GetMessagesByDiscussionRequest request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetByDiscussionId(request.DiscussionId);
            return _mapper.Map<List<MessageDto>>(messages);
        }
    }
}
