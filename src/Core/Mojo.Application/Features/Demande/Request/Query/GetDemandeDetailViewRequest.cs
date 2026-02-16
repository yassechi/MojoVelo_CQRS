using Mojo.Application.DTOs.EntitiesDto.Demande;

namespace Mojo.Application.Features.Demandes.Request.Query
{
    public class GetDemandeDetailViewRequest : IRequest<DemandeDetailDto>
    {
        public int Id { get; set; }
    }
}
