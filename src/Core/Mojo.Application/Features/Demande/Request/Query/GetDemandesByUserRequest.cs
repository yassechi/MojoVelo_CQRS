using Mojo.Application.DTOs.EntitiesDto.Demande;

namespace Mojo.Application.Features.Demandes.Request.Query
{
    public class GetDemandesByUserRequest : IRequest<List<DemandeDto>>
    {
        public string UserId { get; set; } = null!;
    }
}
