using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratsByUserRequest : IRequest<List<ContratDto>>
    {
        public string UserId { get; set; } = null!;
    }
}
