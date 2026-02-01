using Mojo.Domain.Entities;

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
            var discussion = await repository.GetByIdAsync(request.Id);
            if (discussion is null) throw new NotFoundException(nameof(Mojo.Domain.Entities.Discussion), request.Id);

            await repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
