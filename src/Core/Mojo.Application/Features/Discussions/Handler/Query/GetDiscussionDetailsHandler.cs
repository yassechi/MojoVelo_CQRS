namespace Mojo.Application.Features.Discussions.Handler.Query
{
    internal class GetDiscussionDetailsHandler : IRequestHandler<GetDiscussionDetailsRequest, DiscussionDto>
    {
        private readonly IDiscussionRepository repository;
        private readonly IMapper mapper;

        public GetDiscussionDetailsHandler(IDiscussionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DiscussionDto> Handle(GetDiscussionDetailsRequest request, CancellationToken cancellationToken)
        {
            var discussion = await repository.GetByIdAsync(request.Id);

            if (discussion == null)
            {
                throw new Exception("Discussion non trouvée");
            }

            return mapper.Map<DiscussionDto>(discussion);
        }
    }
}
