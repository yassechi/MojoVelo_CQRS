using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.Features.Amortissments.Request.Command
{
    public class UpdateAmortissementCommand : IRequest<BaseResponse>
    {
        public AmortissmentDto dto { get; set; }
    }
}
