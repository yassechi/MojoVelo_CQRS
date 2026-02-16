using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratEditDataRequest : IRequest<ContratEditDataDto>
    {
        public int Id { get; set; }
    }
}
