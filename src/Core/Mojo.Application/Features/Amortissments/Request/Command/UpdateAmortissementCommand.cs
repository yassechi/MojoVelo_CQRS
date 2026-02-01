using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.Features.Amortissments.Request.Command
{
    public class UpdateAmortissementCommand : IRequest<Unit>
    {
        public AmortissmentDto dto { get; set; }
    }
}
