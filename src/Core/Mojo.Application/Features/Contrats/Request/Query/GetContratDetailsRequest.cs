using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratDetailsRequest : IRequest<ContratDto>
    {
        public int Id { get; set; }
    }
}