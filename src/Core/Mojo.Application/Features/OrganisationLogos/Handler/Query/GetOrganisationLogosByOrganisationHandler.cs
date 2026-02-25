using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;
using Mojo.Application.Features.OrganisationLogos.Request.Query;

namespace Mojo.Application.Features.OrganisationLogos.Handler.Query
{
    public class GetOrganisationLogosByOrganisationHandler : IRequestHandler<GetOrganisationLogosByOrganisationRequest, List<OrganisationLogoDto>>
    {
        private readonly IOrganisationLogoRepository _repository;
        private readonly IMapper _mapper;

        public GetOrganisationLogosByOrganisationHandler(IOrganisationLogoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganisationLogoDto>> Handle(GetOrganisationLogosByOrganisationRequest request, CancellationToken cancellationToken)
        {
            if (request.OrganisationId <= 0)
            {
                return new List<OrganisationLogoDto>();
            }

            var logos = await _repository.GetByOrganisationIdAsync(request.OrganisationId);
            return _mapper.Map<List<OrganisationLogoDto>>(logos);
        }
    }
}
