namespace Mojo.Application.Features.Discussions.Handler.Query
{
    public class GetAllDiscussionHandler : IRequestHandler<GetAllDiscussionRequest, List<Mojo.Domain.Entities.Discussion>>
    {
        private readonly IDiscussionRepository repository;
        private readonly IMapper mapper;

        public GetAllDiscussionHandler(IDiscussionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Domain.Entities.Discussion>> Handle(GetAllDiscussionRequest request, CancellationToken cancellationToken)
        {
            var discussions = await repository.GetAllAsync();
            return mapper.Map<List<Mojo.Domain.Entities.Discussion>>(discussions);
        }
    }
}
