using Mojo.Application.DTOs.Dashboard;
using Mojo.Application.Features.Dashboard.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Dashboard.Handler.Query
{
    public class GetUserDashboardHandler : IRequestHandler<GetUserDashboardRequest, UserDashboardDto>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly IContratRepository _contratRepository;

        public GetUserDashboardHandler(
            IDemandeRepository demandeRepository,
            IContratRepository contratRepository)
        {
            _demandeRepository = demandeRepository;
            _contratRepository = contratRepository;
        }

        public async Task<UserDashboardDto> Handle(GetUserDashboardRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return new UserDashboardDto();
            }

            var demandes = (await _demandeRepository.GetByUserIdAsync(request.UserId))
                .Where(d => d.IsActif)
                .ToList();

            var contrats = (await _contratRepository.GetByUserIdAsync(request.UserId))
                .Where(c => c.IsActif)
                .ToList();

            return new UserDashboardDto
            {
                TotalDemandes = demandes.Count,
                TotalContrats = contrats.Count,
                DemandesEnCours = demandes.Count(d => d.Status == DemandeStatus.Encours),
                ContratsActifs = contrats.Count(c => c.StatutContrat == StatutContrat.EnCours),
                DemandesAttente = demandes.Count(d => d.Status == DemandeStatus.AttenteComagnie),
                DemandesAttenteCompagnie = demandes.Count(d => d.Status == DemandeStatus.Finalisation),
                DemandesValide = demandes.Count(d => d.Status == DemandeStatus.Valide),
                ContratsEnCours = contrats.Count(c => c.StatutContrat == StatutContrat.EnCours),
                ContratsTermine = contrats.Count(c => c.StatutContrat == StatutContrat.Termine)
            };
        }
    }
}
