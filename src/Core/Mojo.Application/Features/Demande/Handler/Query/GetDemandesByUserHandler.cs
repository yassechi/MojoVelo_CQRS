using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetDemandesByUserHandler : IRequestHandler<GetDemandesByUserRequest, List<DemandeDto>>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly ILogger<GetDemandesByUserHandler> _logger;

        public GetDemandesByUserHandler(
            IDemandeRepository demandeRepository,
            ILogger<GetDemandesByUserHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<List<DemandeDto>> Handle(GetDemandesByUserRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return new List<DemandeDto>();
            }

            var demandes = await _demandeRepository.GetByUserIdAsync(request.UserId);

            return demandes.Select(d => new DemandeDto
            {
                Id = d.Id,
                Status = d.Status,
                IdUser = d.IdUser,
                IdVelo = d.IdVelo,
                DiscussionId = d.DiscussionId
            }).ToList();
        }
    }
}
