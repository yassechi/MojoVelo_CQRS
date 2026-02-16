using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratListRequest : IRequest<List<AdminContratListItemDto>>
    {
        public string? Type { get; set; }
        public string? Search { get; set; }
        public bool? EndingSoon { get; set; }
        public bool? WithIncidents { get; set; }
        public int? OrganisationId { get; set; }
        public string? UserId { get; set; }
    }
}
