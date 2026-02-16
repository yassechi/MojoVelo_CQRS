using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Request.Query
{
    public class GetOrganisationListRequest : IRequest<List<OrganisationDto>>
    {
        public bool? IsActif { get; set; }
        public string? Search { get; set; }
    }
}
