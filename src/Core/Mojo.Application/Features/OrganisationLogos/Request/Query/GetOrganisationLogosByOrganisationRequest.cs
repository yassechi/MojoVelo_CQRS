using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;

namespace Mojo.Application.Features.OrganisationLogos.Request.Query
{
    public class GetOrganisationLogosByOrganisationRequest : IRequest<List<OrganisationLogoDto>>
    {
        public int OrganisationId { get; set; }
    }
}
