using Mojo.Application.DTOs.EntitiesDto.Velo;

namespace Mojo.Application.Features.Velos.Request.Command
{
    public class UpdateVeloCommand : IRequest<BaseResponse>
    {
        public VeloDto dto { get; set; }
    }
}
