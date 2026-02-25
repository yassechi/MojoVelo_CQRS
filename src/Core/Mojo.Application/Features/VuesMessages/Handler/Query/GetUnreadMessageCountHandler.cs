using Mojo.Application.Features.VuesMessages.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.VuesMessages.Handler.Query
{
    public class GetUnreadMessageCountHandler : IRequestHandler<GetUnreadMessageCountRequest, int>
    {
        private readonly IVuesMessageRepository _vuesMessageRepository;

        public GetUnreadMessageCountHandler(IVuesMessageRepository vuesMessageRepository)
        {
            _vuesMessageRepository = vuesMessageRepository;
        }

        public async Task<int> Handle(GetUnreadMessageCountRequest request, CancellationToken cancellationToken)
        {
            return await _vuesMessageRepository.GetUnreadCountAsync(
                request.UserId,
                request.Role,
                request.OrganisationId);
        }
    }
}
