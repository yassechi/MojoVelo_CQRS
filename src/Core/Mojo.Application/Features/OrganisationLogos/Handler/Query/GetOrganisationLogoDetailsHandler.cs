using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;
using Mojo.Application.Features.OrganisationLogos.Request.Query;

namespace Mojo.Application.Features.OrganisationLogos.Handler.Query
{
    public class GetOrganisationLogoDetailsHandler : IRequestHandler<GetOrganisationLogoDetailsRequest, OrganisationLogoDto>
    {
        private readonly IOrganisationLogoRepository _repository;
        private readonly IMapper _mapper;

        public GetOrganisationLogoDetailsHandler(IOrganisationLogoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrganisationLogoDto> Handle(GetOrganisationLogoDetailsRequest request, CancellationToken cancellationToken)
        {
            var logo = await _repository.GetByIdAsync(request.Id);
            if (logo == null || !logo.IsActif)
            {
                throw new NotFoundException(nameof(OrganisationLogo), request.Id);
            }
            return _mapper.Map<OrganisationLogoDto>(logo);
        }
    }
}
