using Mojo.Application.DTOs.EntitiesDto.Intervention;

namespace Mojo.Application.Features.Interventions.Request.Command
{
    public class UpdateInterventionCommand : IRequest<BaseResponse>
    {
        public InterventionDto dto { get; set; }
    }
}
