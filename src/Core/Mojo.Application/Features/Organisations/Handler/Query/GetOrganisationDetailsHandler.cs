
namespace Mojo.Application.Features.Organisations.Handler.Query
{
    internal class GetOrganisationDetailsHandler : IRequestHandler<GetOrganisationDetailsRequest, OrganisationDto>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public GetOrganisationDetailsHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<OrganisationDto> Handle(GetOrganisationDetailsRequest request, CancellationToken cancellationToken)
        {
            Organisation organisation = await repository.GetByIdAsync(request.Id);

            if (organisation == null)
            {
                throw new Exception("Organisation non trouvée");
            }

            return mapper.Map<OrganisationDto>(organisation);
        }
    }
}
