using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetDemandesByOrganisationHandler : IRequestHandler<GetDemandesByOrganisationRequest, List<DemandeDto>>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly ILogger<GetDemandesByOrganisationHandler> _logger;

        public GetDemandesByOrganisationHandler(
            IDemandeRepository demandeRepository,
            ILogger<GetDemandesByOrganisationHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<List<DemandeDto>> Handle(GetDemandesByOrganisationRequest request, CancellationToken cancellationToken)
        {
            if (request.OrganisationId <= 0)
            {
                return new List<DemandeDto>();
            }

            var demandes = await _demandeRepository.GetByOrganisationIdAsync(request.OrganisationId);

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
