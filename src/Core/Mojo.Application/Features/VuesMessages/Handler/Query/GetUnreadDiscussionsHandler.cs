using Mojo.Application.Features.VuesMessages.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.VuesMessages.Handler.Query
{
    public class GetUnreadDiscussionsHandler : IRequestHandler<GetUnreadDiscussionsRequest, List<int>>
    {
        private readonly IVuesMessageRepository _vuesMessageRepository;

        public GetUnreadDiscussionsHandler(IVuesMessageRepository vuesMessageRepository)
        {
            _vuesMessageRepository = vuesMessageRepository;
        }

        public async Task<List<int>> Handle(GetUnreadDiscussionsRequest request, CancellationToken cancellationToken)
        {
            return await _vuesMessageRepository.GetUnreadDiscussionIdsAsync(
                request.UserId,
                request.Role,
                request.OrganisationId);
        }
    }
}
