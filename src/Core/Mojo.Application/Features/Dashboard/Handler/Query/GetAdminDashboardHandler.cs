using Mojo.Application.DTOs.Dashboard;
using Mojo.Application.Features.Dashboard.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Dashboard.Handler.Query
{
    public class GetAdminDashboardHandler : IRequestHandler<GetAdminDashboardRequest, AdminDashboardDto>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly IContratRepository _contratRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;

        public GetAdminDashboardHandler(
            IDemandeRepository demandeRepository,
            IContratRepository contratRepository,
            IUserRepository userRepository,
            IVeloRepository veloRepository)
        {
            _demandeRepository = demandeRepository;
            _contratRepository = contratRepository;
            _userRepository = userRepository;
            _veloRepository = veloRepository;
        }

        public async Task<AdminDashboardDto> Handle(GetAdminDashboardRequest request, CancellationToken cancellationToken)
        {
            var demandes = (await _demandeRepository.GetAllAsync()).Where(d => d.IsActif).ToList();
            var contrats = (await _contratRepository.GetAllAsync()).Where(c => c.IsActif).ToList();
            var users = (await _userRepository.GetAllAsync()).Where(u => u.IsActif).ToList();
            var velos = (await _veloRepository.GetAllAsync()).Where(v => v.IsActif).ToList();

            var dto = new AdminDashboardDto
            {
                PendingDemandes = demandes.Count(d => d.Status == DemandeStatus.AttenteComagnie),
                ActiveContrats = contrats.Count(c => c.StatutContrat == StatutContrat.EnCours),
                BudgetTotal = contrats.Sum(c => (c.LoyerMensuelHT) * (c.Duree == 0 ? 36 : c.Duree))
            };

            var usersById = users.ToDictionary(u => u.Id);
            var velosById = velos.ToDictionary(v => v.Id);

            var typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var demande in demandes)
            {
                if (!velosById.TryGetValue(demande.IdVelo, out var velo))
                {
                    continue;
                }
                var label = string.IsNullOrWhiteSpace(velo.Type) ? "Autre" : velo.Type.Trim();
                typeCounts[label] = (typeCounts.TryGetValue(label, out var current) ? current : 0) + 1;
            }

            dto.BikeTypeCounts = typeCounts
                .OrderByDescending(kv => kv.Value)
                .Select(kv => new BikeTypeCountDto
                {
                    Label = kv.Key,
                    Value = kv.Value
                })
                .ToList();

            var recentDemandes = demandes
                .OrderByDescending(d => d.Id)
                .Take(3);

            foreach (var demande in recentDemandes)
            {
                var userName = usersById.TryGetValue(demande.IdUser, out var user)
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : demande.IdUser;
                var bikeTitle = velosById.TryGetValue(demande.IdVelo, out var velo)
                    ? velo.Modele
                    : $"#{demande.IdVelo}";

                dto.ActivityFeed.Add(new ActivityFeedItemDto
                {
                    Title = $"Demande #{demande.Id}",
                    Detail = $"{userName} - {bikeTitle}",
                    Time = demande.CreatedDate == default ? "En cours" : demande.CreatedDate.ToString("yyyy-MM-dd")
                });
            }

            var recentContrats = contrats
                .OrderByDescending(c => c.DateDebut)
                .Take(2);

            foreach (var contrat in recentContrats)
            {
                var userName = usersById.TryGetValue(contrat.BeneficiaireId, out var user)
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : contrat.BeneficiaireId;
                var bikeTitle = velosById.TryGetValue(contrat.VeloId, out var velo)
                    ? velo.Modele
                    : $"#{contrat.VeloId}";

                dto.ActivityFeed.Add(new ActivityFeedItemDto
                {
                    Title = $"Contrat {contrat.Ref}",
                    Detail = $"{userName} - {bikeTitle}",
                    Time = contrat.DateDebut.ToString("yyyy-MM-dd")
                });
            }

            return dto;
        }
    }
}
