using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.Features.Amortissments.Request.Command
{
    public class CreateAmortissementCommand : IRequest<Unit>
    {
        public AmortissmentDto amortissmentDto { get; set; }
    }
}
