using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratListHandler : IRequestHandler<GetContratListRequest, List<AdminContratListItemDto>>
    {
        private readonly IContratRepository _contratRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IVeloRepository _veloRepository;
        private readonly IInterventionRepository _interventionRepository;

        public GetContratListHandler(
            IContratRepository contratRepository,
            IUserRepository userRepository,
            IOrganisationRepository organisationRepository,
            IVeloRepository veloRepository,
            IInterventionRepository interventionRepository)
        {
            _contratRepository = contratRepository;
            _userRepository = userRepository;
            _organisationRepository = organisationRepository;
            _veloRepository = veloRepository;
            _interventionRepository = interventionRepository;
        }

        public async Task<List<AdminContratListItemDto>> Handle(GetContratListRequest request, CancellationToken cancellationToken)
        {
            var contrats = await _contratRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var organisations = await _organisationRepository.GetAllAsync();
            var velos = await _veloRepository.GetAllAsync();
            var interventions = await _interventionRepository.GetAllAsync();

            var activeContrats = contrats.Where(c => c.IsActif).ToList();
            var activeUsers = users.Where(u => u.IsActif).ToList();
            var activeOrganisations = organisations.Where(o => o.IsActif).ToList();
            var activeVelos = velos.Where(v => v.IsActif).ToList();
            var activeInterventions = interventions.Where(i => i.IsActif).ToList();

            var usersById = activeUsers.ToDictionary(u => u.Id);
            var orgsById = activeOrganisations.ToDictionary(o => o.Id);
            var velosById = activeVelos.ToDictionary(v => v.Id);

            var incidentsByVelo = activeInterventions
                .GroupBy(i => i.VeloId)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        Count = g.Count(),
                        Used = g.Sum(x => x.Cout)
                    });

            var typeFilter = Normalize(request.Type);
            var search = Normalize(request.Search);
            var userIdFilter = request.UserId?.Trim();

            var result = new List<AdminContratListItemDto>();

            foreach (var contrat in activeContrats)
            {
                if (!string.IsNullOrWhiteSpace(userIdFilter) &&
                    !string.Equals(contrat.BeneficiaireId, userIdFilter, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                usersById.TryGetValue(contrat.BeneficiaireId, out var user);
                usersById.TryGetValue(contrat.UserRhId, out var userRh);
                var organisationId = user?.OrganisationId ?? 0;

                if (request.OrganisationId.HasValue && organisationId != request.OrganisationId.Value)
                {
                    continue;
                }

                velosById.TryGetValue(contrat.VeloId, out var velo);
                var veloType = Normalize(velo?.Type);

                if (!string.IsNullOrWhiteSpace(typeFilter) && veloType != typeFilter)
                {
                    continue;
                }

                var userName = user == null ? string.Empty : $"{user.FirstName} {user.LastName}".Trim();
                var userRhName = userRh == null ? string.Empty : $"{userRh.FirstName} {userRh.LastName}".Trim();
                var veloModele = velo?.Modele ?? string.Empty;
                var veloMarque = velo?.Marque ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchHit =
                        ContainsNormalized(userName, search) ||
                        ContainsNormalized(veloModele, search) ||
                        ContainsNormalized(veloMarque, search);
                    if (!searchHit)
                    {
                        continue;
                    }
                }

                var incidents = incidentsByVelo.TryGetValue(contrat.VeloId, out var incidentData)
                    ? incidentData
                    : new { Count = 0, Used = 0m };

                if (request.WithIncidents == true && incidents.Count == 0)
                {
                    continue;
                }

                var budget = CalculateMaintenanceBudget(velo?.PrixAchat ?? 0m);
                var used = incidents.Used;
                var progress = budget <= 0 ? 0 : Math.Min(100, (int)Math.Round((used / budget) * 100m));

                var isEndingSoon = IsEndingSoon(contrat.DateFin);
                if (request.EndingSoon == true && !isEndingSoon)
                {
                    continue;
                }

                var organisationName = orgsById.TryGetValue(organisationId, out var org)
                    ? org.Name
                    : string.Empty;

                result.Add(new AdminContratListItemDto
                {
                    Id = contrat.Id,
                    Ref = contrat.Ref ?? string.Empty,
                    BeneficiaireId = contrat.BeneficiaireId,
                    BeneficiaireName = userName,
                    UserRhId = contrat.UserRhId ?? string.Empty,
                    UserRhName = userRhName,
                    OrganisationId = organisationId,
                    OrganisationName = organisationName,
                    VeloId = contrat.VeloId,
                    VeloMarque = velo?.Marque ?? string.Empty,
                    VeloModele = velo?.Modele ?? string.Empty,
                    VeloType = velo?.Type,
                    VeloPrixAchat = velo?.PrixAchat ?? 0m,
                    DateDebut = contrat.DateDebut,
                    DateFin = contrat.DateFin,
                    LoyerMensuelHT = contrat.LoyerMensuelHT,
                    Duree = contrat.Duree,
                    StatutContrat = contrat.StatutContrat,
                    IncidentsCount = incidents.Count,
                    MaintenanceBudget = budget,
                    MaintenanceUsed = used,
                    MaintenanceProgress = progress,
                    IsEndingSoon = isEndingSoon
                });
            }

            return result;
        }

        private static decimal CalculateMaintenanceBudget(decimal bikePrice)
        {
            var budget = Math.Round(bikePrice * 0.05m);
            return Math.Max(300m, budget);
        }

        private static bool IsEndingSoon(DateOnly dateFin)
        {
            var end = dateFin.ToDateTime(TimeOnly.MinValue);
            var diffDays = (end - DateTime.Now).TotalDays;
            return diffDays <= 90;
        }

        private static string Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
        }

        private static bool ContainsNormalized(string source, string search)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(search))
            {
                return false;
            }
            return source.Trim().ToLowerInvariant().Contains(search);
        }
    }
}
