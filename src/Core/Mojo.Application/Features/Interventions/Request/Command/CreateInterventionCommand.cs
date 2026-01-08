using Mojo.Application.DTOs.EntitiesDto.Intervention;

namespace Mojo.Application.Features.Interventions.Request.Command
{
    public class CreateInterventionCommand : IRequest<Unit>
    {
        public InterventionDto dto { get; set; }
    }
}
