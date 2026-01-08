using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Handler.Query
{
    public class GetAllOrganisationHandler : IRequestHandler<GetAllOrganisationRequest, List<OrganisationDto>>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public GetAllOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<OrganisationDto>> Handle(GetAllOrganisationRequest request, CancellationToken cancellationToken)
        {
            var organisations = await repository.GetAllAsync();
            return mapper.Map<List<OrganisationDto>>(organisations);
        }
    }
}
