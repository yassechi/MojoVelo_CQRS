using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetDemandeListHandler : IRequestHandler<GetDemandeListRequest, List<AdminDemandeListItemDto>>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IVeloRepository _veloRepository;

        public GetDemandeListHandler(
            IDemandeRepository demandeRepository,
            IUserRepository userRepository,
            IOrganisationRepository organisationRepository,
            IVeloRepository veloRepository)
        {
            _demandeRepository = demandeRepository;
            _userRepository = userRepository;
            _organisationRepository = organisationRepository;
            _veloRepository = veloRepository;
        }

        public async Task<List<AdminDemandeListItemDto>> Handle(GetDemandeListRequest request, CancellationToken cancellationToken)
        {
            var demandes = await _demandeRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var organisations = await _organisationRepository.GetAllAsync();
            var velos = await _veloRepository.GetAllAsync();

            var activeDemandes = demandes.Where(d => d.IsActif).ToList();
            var activeUsers = users.Where(u => u.IsActif).ToList();
            var activeOrganisations = organisations.Where(o => o.IsActif).ToList();
            var activeVelos = velos.Where(v => v.IsActif).ToList();

            var usersById = activeUsers.ToDictionary(u => u.Id);
            var orgsById = activeOrganisations.ToDictionary(o => o.Id);
            var velosById = activeVelos.ToDictionary(v => v.Id);

            var statusFilter = request.Status.HasValue ? (DemandeStatus?)request.Status.Value : null;
            var typeFilter = Normalize(request.Type);
            var search = Normalize(request.Search);

            var result = new List<AdminDemandeListItemDto>();

            foreach (var demande in activeDemandes)
            {
                if (statusFilter.HasValue && demande.Status != statusFilter.Value)
                {
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(request.UserId) && demande.IdUser != request.UserId)
                {
                    continue;
                }

                usersById.TryGetValue(demande.IdUser, out var user);
                var organisationId = user?.OrganisationId ?? 0;

                if (request.OrganisationId.HasValue && organisationId != request.OrganisationId.Value)
                {
                    continue;
                }

                velosById.TryGetValue(demande.IdVelo, out var velo);
                var veloType = Normalize(velo?.Type);

                if (!string.IsNullOrWhiteSpace(typeFilter) && veloType != typeFilter)
                {
                    continue;
                }

                var userName = user == null ? string.Empty : $"{user.FirstName} {user.LastName}".Trim();
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

                var organisationName = orgsById.TryGetValue(organisationId, out var org)
                    ? org.Name
                    : string.Empty;

                result.Add(new AdminDemandeListItemDto
                {
                    Id = demande.Id,
                    Status = demande.Status,
                    IdUser = demande.IdUser,
                    UserName = userName,
                    OrganisationId = organisationId,
                    OrganisationName = organisationName,
                    IdVelo = demande.IdVelo,
                    VeloMarque = velo?.Marque ?? string.Empty,
                    VeloModele = velo?.Modele ?? string.Empty,
                    VeloType = velo?.Type,
                    VeloPrixAchat = velo?.PrixAchat,
                    DiscussionId = demande.DiscussionId,
                    CreatedAt = demande.CreatedDate
                });
            }

            return result;
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
