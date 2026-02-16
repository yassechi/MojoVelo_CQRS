namespace Mojo.Application.Features.Interventions.Handler.Query
{
    public class GetInterventionsByVeloHandler : IRequestHandler<GetInterventionsByVeloRequest, List<Intervention>>
    {
        private readonly IInterventionRepository _repository;

        public GetInterventionsByVeloHandler(IInterventionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Intervention>> Handle(GetInterventionsByVeloRequest request, CancellationToken cancellationToken)
        {
            if (request.VeloId <= 0)
            {
                return new List<Intervention>();
            }

            return await _repository.GetByVeloIdAsync(request.VeloId);
        }
    }
}
