namespace Mojo.Application.Features.Messages.Handler.Command
{
    internal class CreateMessageHandle : IRequestHandler<CreateMessageCommand, Unit>
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public CreateMessageHandle(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = mapper.Map<Message>(request.dto);

            await repository.CreateAsync(message);

            return Unit.Value;
        }
    }
}
