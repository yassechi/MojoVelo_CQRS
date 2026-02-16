using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratsByOrganisationRequest : IRequest<List<ContratDto>>
    {
        public int OrganisationId { get; set; }
    }
}
