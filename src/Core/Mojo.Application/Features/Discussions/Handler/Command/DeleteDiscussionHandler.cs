namespace Mojo.Application.Features.Discussions.Handler.Command
{
    public class DeleteDiscussionHandler : IRequestHandler<DeleteDiscussionCommand, Unit>
    {
        private readonly IDiscussionRepository repository;
        private readonly IMapper mapper;

        public DeleteDiscussionHandler(IDiscussionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDiscussionCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Discussion discussion = await repository.GetByIdAsync(request.Id);

            if (discussion != null)
            {
                await repository.DeleteAsync(request.Id);
            }

            return Unit.Value;
        }
    }
}
