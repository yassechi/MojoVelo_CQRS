using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;

namespace Mojo.Application.Features.OrganisationLogos.Request.Query
{
    public class GetOrganisationLogoDetailsRequest : IRequest<OrganisationLogoDto>
    {
        public int Id { get; set; }
    }
}
