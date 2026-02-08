using Mojo.Application.DTOs.EntitiesDto.Message;

namespace Mojo.Application.Features.Messages.Handler.Query
{
    public class GetAllMessageHandler : IRequestHandler<GetAllMessageRequest, List<MessageDto>>
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public GetAllMessageHandler(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<MessageDto>> Handle(GetAllMessageRequest request, CancellationToken cancellationToken)
        {
            var messages = await repository.GetAllAsync();

            // Filtrer uniquement les messages actifs
            var messagesActifs = messages.Where(m => m.IsActif).ToList();

            return mapper.Map<List<MessageDto>>(messagesActifs);
        }
    }
}