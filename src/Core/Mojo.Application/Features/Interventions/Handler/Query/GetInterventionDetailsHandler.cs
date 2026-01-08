namespace Mojo.Application.Features.Interventions.Handler.Query
{
    internal class GetInterventionDetailsHandler : IRequestHandler<GetInterventionDetailsRequest, InterventionDto>
    {
        private readonly IInterventionRepository repository;
        private readonly IMapper mapper;

        public GetInterventionDetailsHandler(IInterventionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<InterventionDto> Handle(GetInterventionDetailsRequest request, CancellationToken cancellationToken)
        {
            var intervention = await repository.GetByIdAsync(request.Id);

            if (intervention == null)
            {
                throw new Exception("Discussion non trouvée");
            }

            return mapper.Map<InterventionDto>(intervention);
        }
    }
}
