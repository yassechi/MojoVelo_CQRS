using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;
using Mojo.Application.Features.OrganisationLogos.Request.Query;

namespace Mojo.Application.Features.OrganisationLogos.Handler.Query
{
    public class GetAllOrganisationLogoHandler : IRequestHandler<GetAllOrganisationLogoRequest, List<OrganisationLogoDto>>
    {
        private readonly IOrganisationLogoRepository _repository;
        private readonly IMapper _mapper;

        public GetAllOrganisationLogoHandler(IOrganisationLogoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganisationLogoDto>> Handle(GetAllOrganisationLogoRequest request, CancellationToken cancellationToken)
        {
            var logos = await _repository.GetAllAsync();
            var actifs = logos.Where(l => l.IsActif).ToList();
            return _mapper.Map<List<OrganisationLogoDto>>(actifs);
        }
    }
}
