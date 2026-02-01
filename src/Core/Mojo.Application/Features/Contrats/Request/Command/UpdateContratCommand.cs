using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Command
{
    public class UpdateContratCommand : IRequest<Unit>
    {
        public ContratDto dto { get; set; }
    }
}
