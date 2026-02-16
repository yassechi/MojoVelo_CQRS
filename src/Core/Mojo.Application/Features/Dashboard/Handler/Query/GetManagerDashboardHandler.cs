using Mojo.Application.DTOs.Dashboard;
using Mojo.Application.Features.Dashboard.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Dashboard.Handler.Query
{
    public class GetManagerDashboardHandler : IRequestHandler<GetManagerDashboardRequest, ManagerDashboardDto>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly IContratRepository _contratRepository;
        private readonly IUserRepository _userRepository;

        public GetManagerDashboardHandler(
            IDemandeRepository demandeRepository,
            IContratRepository contratRepository,
            IUserRepository userRepository)
        {
            _demandeRepository = demandeRepository;
            _contratRepository = contratRepository;
            _userRepository = userRepository;
        }

        public async Task<ManagerDashboardDto> Handle(GetManagerDashboardRequest request, CancellationToken cancellationToken)
        {
            if (request.OrganisationId <= 0)
            {
                return new ManagerDashboardDto();
            }

            var users = (await _userRepository.GetAllAsync())
                .Where(u => u.IsActif && u.OrganisationId == request.OrganisationId)
                .ToList();

            var userIds = users.Select(u => u.Id).ToHashSet();

            var demandes = (await _demandeRepository.GetAllAsync())
                .Where(d => d.IsActif && userIds.Contains(d.IdUser))
                .ToList();

            var contrats = (await _contratRepository.GetAllAsync())
                .Where(c => c.IsActif && userIds.Contains(c.BeneficiaireId))
                .ToList();

            return new ManagerDashboardDto
            {
                TotalEmployes = users.Count(u => u.Role == 3),
                TotalDemandes = demandes.Count,
                TotalContrats = contrats.Count,
                DemandesEnCours = demandes.Count(d => d.Status == DemandeStatus.Encours),
                DemandesAttente = demandes.Count(d => d.Status == DemandeStatus.AttenteComagnie),
                DemandesAttenteCompagnie = demandes.Count(d => d.Status == DemandeStatus.Finalisation),
                DemandesValide = demandes.Count(d => d.Status == DemandeStatus.Valide),
                ContratsEnCours = contrats.Count(c => c.StatutContrat == StatutContrat.EnCours),
                ContratsTermine = contrats.Count(c => c.StatutContrat == StatutContrat.Termine)
            };
        }
    }
}
