namespace Mojo.Application.Features.Interventions.Handler.Query
{
    public class GetAllInterventionHandler : IRequestHandler<GetAllInterventionRequest, List<Intervention>>
    {
        private readonly IInterventionRepository repository;
        private readonly IMapper mapper;

        public GetAllInterventionHandler(IInterventionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Intervention>> Handle(GetAllInterventionRequest request, CancellationToken cancellationToken)
        {
            var interventions = await repository.GetAllAsync();

            // Filtrer uniquement les interventions actives
            var interventionsActives = interventions.Where(i => i.IsActif).ToList();

            return mapper.Map<List<Mojo.Domain.Entities.Intervention>>(interventionsActives);
        }
    }
}