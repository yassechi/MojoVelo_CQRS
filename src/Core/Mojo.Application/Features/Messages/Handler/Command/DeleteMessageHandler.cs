
namespace Mojo.Application.Features.Messages.Handler.Command
{
    internal class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, Unit>
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public DeleteMessageHandler(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await repository.GetByIdAsync(request.Id);

            if (message != null)
            {
                await repository.DeleteAsync(request.Id);
            }

            return Unit.Value;
        }
    }
}
