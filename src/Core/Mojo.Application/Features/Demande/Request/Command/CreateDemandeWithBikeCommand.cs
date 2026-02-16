using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Reponses;

namespace Mojo.Application.Features.Demandes.Request.Command
{
    public class CreateDemandeWithBikeCommand : IRequest<CreateDemandeWithBikeResponse>
    {
        public CreateDemandeWithBikeDto dto { get; set; } = null!;
    }
}
