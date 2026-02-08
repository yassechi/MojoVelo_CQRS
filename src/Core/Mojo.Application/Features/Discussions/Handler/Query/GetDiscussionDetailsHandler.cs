using Mojo.Application.DTOs.EntitiesDto.Discussion;
using Mojo.Application.Exceptions;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Discussions.Handler.Query
{
    public class GetDiscussionDetailsHandler : IRequestHandler<GetDiscussionDetailsRequest, DiscussionDto>
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

            if (discussion == null || !discussion.IsActif)
            {
                throw new NotFoundException(nameof(Discussion), request.Id);
            }

            return mapper.Map<DiscussionDto>(discussion);
        }
    }
}