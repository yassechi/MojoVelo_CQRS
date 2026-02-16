using Mojo.Application.DTOs.EntitiesDto.Demande;

namespace Mojo.Application.Features.Demandes.Request.Query
{
    public class GetDemandesByOrganisationRequest : IRequest<List<DemandeDto>>
    {
        public int OrganisationId { get; set; }
    }
}
