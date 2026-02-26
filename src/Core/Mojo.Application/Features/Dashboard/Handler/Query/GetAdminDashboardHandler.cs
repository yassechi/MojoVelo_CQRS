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

            var today = DateOnly.FromDateTime(DateTime.Today);
            var activity = new List<ActivityFeedItemDto>();

            foreach (var d in demandes
                .OrderByDescending(d => d.CreatedDate)
                .ThenByDescending(d => d.Id)
                .Take(5))
            {
                var user = users.FirstOrDefault(u => u.Id == d.IdUser);
                var velo = velos.FirstOrDefault(v => v.Id == d.IdVelo);
                var userName = user != null ? user.FirstName + " " + user.LastName : d.IdUser;
                var bikeTitle = velo?.Modele ?? "#" + d.IdVelo;
                var time = d.CreatedDate == default ? "En cours" : d.CreatedDate.ToString("yyyy-MM-dd");

                activity.Add(new ActivityFeedItemDto
                {
                    Title = "Demande #" + d.Id,
                    Detail = userName + " - " + bikeTitle,
                    Time = time
                });
            }

            return new AdminDashboardDto
            {
                PendingDemandes = demandes.Count(d => d.Status == DemandeStatus.AttenteComagnie),
                ActiveContrats = contrats.Count(c => c.StatutContrat == StatutContrat.EnCours),
                ExpiringContrats = contrats.Count(c =>
                    c.StatutContrat == StatutContrat.EnCours &&
                    c.DateFin >= today &&
                    c.DateFin <= today.AddDays(30)),
                ActivityFeed = activity
            };
        }
    }
}
