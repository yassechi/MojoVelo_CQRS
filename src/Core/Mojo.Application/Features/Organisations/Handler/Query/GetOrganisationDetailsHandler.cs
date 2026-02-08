using Mojo.Application.DTOs.EntitiesDto.Organisation;
using Mojo.Application.Exceptions;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Organisations.Handler.Query
{
    public class GetOrganisationDetailsHandler : IRequestHandler<GetOrganisationDetailsRequest, OrganisationDto>
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
            var organisation = await repository.GetByIdAsync(request.Id);

            if (organisation == null || !organisation.IsActif)
            {
                throw new NotFoundException(nameof(Organisation), request.Id);
            }

            return mapper.Map<OrganisationDto>(organisation);
        }
    }
}