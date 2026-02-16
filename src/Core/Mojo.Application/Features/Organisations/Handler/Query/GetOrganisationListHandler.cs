using Mojo.Application.DTOs.EntitiesDto.Organisation;
using Mojo.Application.Features.Organisations.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Organisations.Handler.Query
{
    public class GetOrganisationListHandler : IRequestHandler<GetOrganisationListRequest, List<OrganisationDto>>
    {
        private readonly IOrganisationRepository _repository;
        private readonly IMapper _mapper;

        public GetOrganisationListHandler(IOrganisationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganisationDto>> Handle(GetOrganisationListRequest request, CancellationToken cancellationToken)
        {
            var organisations = await _repository.GetAllAsync();
            var search = Normalize(request.Search);

            var filtered = organisations.Where(org =>
            {
                if (request.IsActif.HasValue && org.IsActif != request.IsActif.Value)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(search))
                {
                    return true;
                }

                return ContainsNormalized(org.Name, search) ||
                       ContainsNormalized(org.Code, search) ||
                       ContainsNormalized(org.EmailAutorise, search) ||
                       ContainsNormalized(org.ContactEmail, search);
            }).ToList();

            return _mapper.Map<List<OrganisationDto>>(filtered);
        }

        private static string Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
        }

        private static bool ContainsNormalized(string? source, string search)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(search))
            {
                return false;
            }
            return source.Trim().ToLowerInvariant().Contains(search);
        }
    }
}
