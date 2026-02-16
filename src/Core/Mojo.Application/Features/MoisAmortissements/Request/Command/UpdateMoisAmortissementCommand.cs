using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;

namespace Mojo.Application.Features.MoisAmortissements.Request.Command
{
    public class UpdateMoisAmortissementCommand : IRequest<BaseResponse>
    {
        public MoisAmortissementDto dto { get; set; } = null!;
    }
}
