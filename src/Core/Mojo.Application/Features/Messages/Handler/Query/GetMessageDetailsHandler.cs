using Mojo.Application.DTOs.EntitiesDto.Message;
using Mojo.Application.Exceptions;
using Mojo.Domain.Entities;

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

            if (message == null || !message.IsActif)
            {
                throw new NotFoundException(nameof(Message), request.Id);
            }

            return mapper.Map<MessageDto>(message);
        }
    }
}