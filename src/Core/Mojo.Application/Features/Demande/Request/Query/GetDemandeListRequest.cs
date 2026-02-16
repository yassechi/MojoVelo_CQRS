using Mojo.Application.DTOs.EntitiesDto.Demande;

namespace Mojo.Application.Features.Demandes.Request.Query
{
    public class GetDemandeListRequest : IRequest<List<AdminDemandeListItemDto>>
    {
        public int? Status { get; set; }
        public string? Type { get; set; }
        public string? Search { get; set; }
        public int? OrganisationId { get; set; }
        public string? UserId { get; set; }
    }
}
