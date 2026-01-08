
namespace Mojo.Application.Features.Messages.Handler.Command
{
    internal class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, Unit>
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public UpdateMessageHandler(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var oldMessage = await repository.GetByIdAsync(request.dto.Id);
            var updatedMessage = mapper.Map(request.dto, oldMessage);
            await repository.UpadteAsync(updatedMessage);
            return Unit.Value;
        }
    }
}
