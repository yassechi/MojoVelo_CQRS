using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratDetailViewRequest : IRequest<ContratDetailDto>
    {
        public int Id { get; set; }
    }
}
