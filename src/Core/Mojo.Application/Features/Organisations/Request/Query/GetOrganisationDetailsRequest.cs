using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Request.Query
{
    public class GetOrganisationDetailsRequest : IRequest<OrganisationDto>
    {
        public int Id { get; set; }
    }
}
