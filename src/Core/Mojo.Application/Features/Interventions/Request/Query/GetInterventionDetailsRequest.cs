using Mojo.Application.DTOs.EntitiesDto.Intervention;

namespace Mojo.Application.Features.Interventions.Request.Query
{
    public class GetInterventionDetailsRequest : IRequest<InterventionDto>
    {
        public int Id { get; set; }
    }
}
