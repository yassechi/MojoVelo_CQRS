using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Command
{
    public class CreateContratCommand : IRequest
    {
        public ContratDto dto { get; set; }
    }
}
