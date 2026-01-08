using Mojo.Application.DTOs.EntitiesDto.Velo;

namespace Mojo.Application.Features.Velos.Request.Command
{
    public class UpdateVeloCommand : IRequest<Unit>
    {
        public VeloDto dto { get; set; }
    }
}
