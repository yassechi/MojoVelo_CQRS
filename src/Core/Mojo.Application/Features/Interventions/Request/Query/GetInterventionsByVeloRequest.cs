namespace Mojo.Application.Features.Interventions.Request.Query
{
    public class GetInterventionsByVeloRequest : IRequest<List<Intervention>>
    {
        public int VeloId { get; set; }
    }
}
