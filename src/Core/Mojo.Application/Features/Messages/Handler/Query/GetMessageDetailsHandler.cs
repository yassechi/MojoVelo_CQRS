using Mojo.Application.DTOs.EntitiesDto.Message;

namespace Mojo.Application.Features.Messages.Handler.Query
{
    public class GetMessageDetailsHandler : IRequestHandler<GetMessageDetailsRequest, MessageDto>
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public GetMessageDetailsHandler(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MessageDto> Handle(GetMessageDetailsRequest request, CancellationToken cancellationToken)
        {
            var message = await repository.GetByIdAsync(request.Id);

            if (message == null)
            {
                throw new Exception("Discussion non trouvée");
            }

            return mapper.Map<MessageDto>(message);
        }
    }
}
