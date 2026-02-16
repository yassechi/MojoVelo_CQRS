using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Request.Query
{
    public class ResolveOrganisationRequest : IRequest<OrganisationDto?>
    {
        public string EmailOrDomain { get; set; } = null!;
    }
}
