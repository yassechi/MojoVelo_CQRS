using Mojo.Application.DTOs.EntitiesDto.Contrat;

namespace Mojo.Application.Features.Contrats.Request.Command
{
    public class CreateContratCommand : IRequest<BaseResponse>
    {
        public ContratDto dto { get; set; }
    }
}
