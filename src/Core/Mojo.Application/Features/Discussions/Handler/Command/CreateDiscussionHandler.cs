
namespace Mojo.Application.Features.Discussion.Handler.Command
{
    internal class CreateDiscussionHandler : IRequestHandler<CreateDiscussionCommand, Unit>
    {
        private readonly IDiscussionRepository repository;
        private readonly IMapper mapper;

        public CreateDiscussionHandler(IDiscussionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public  async Task<Unit> Handle(CreateDiscussionCommand request, CancellationToken cancellationToken)
        {
            var discussion = mapper.Map<Mojo.Domain.Entities.Discussion>(request.dto);

            await repository.CreateAsync(discussion);

            return Unit.Value;
        }
    }
}
