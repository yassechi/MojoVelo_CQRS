using Mojo.Application.DTOs.EntitiesDto.Organisation;
using Mojo.Application.Features.Organisations.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Organisations.Handler.Query
{
    public class ResolveOrganisationHandler : IRequestHandler<ResolveOrganisationRequest, OrganisationDto?>
    {
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IMapper _mapper;

        public ResolveOrganisationHandler(IOrganisationRepository organisationRepository, IMapper mapper)
        {
            _organisationRepository = organisationRepository;
            _mapper = mapper;
        }

        public async Task<OrganisationDto?> Handle(ResolveOrganisationRequest request, CancellationToken cancellationToken)
        {
            var domain = ExtractDomain(request.EmailOrDomain);
            if (string.IsNullOrWhiteSpace(domain))
            {
                return null;
            }

            var organisations = await _organisationRepository.GetAllAsync();
            if (organisations == null || organisations.Count == 0)
            {
                return null;
            }

            var active = organisations.Where(o => o.IsActif).ToList();

            var match = FindMatchingOrganisation(active, domain) ?? FindMatchingOrganisation(organisations, domain);
            return match == null ? null : _mapper.Map<OrganisationDto>(match);
        }

        private static Organisation? FindMatchingOrganisation(List<Organisation> organisations, string userDomain)
        {
            return organisations.FirstOrDefault((org) =>
            {
                var allowedDomains = ExtractDomains(org.EmailAutorise ?? string.Empty);
                return allowedDomains.Any(domain => MatchesDomain(userDomain, domain));
            });
        }

        private static List<string> ExtractDomains(string value)
        {
            return value
                .Split(new[] { ',', ';', ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(ExtractDomain)
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .ToList()!;
        }

        private static string? ExtractDomain(string? value)
        {
            var trimmed = (value ?? string.Empty).Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                return null;
            }

            var atIndex = trimmed.LastIndexOf('@');
            var raw = atIndex >= 0 ? trimmed[(atIndex + 1)..] : trimmed;
            var domain = raw.TrimStart('@');
            return string.IsNullOrWhiteSpace(domain) ? null : domain;
        }

        private static bool MatchesDomain(string userDomain, string allowedDomain)
        {
            return userDomain == allowedDomain || userDomain.EndsWith($".{allowedDomain}");
        }
    }
}
