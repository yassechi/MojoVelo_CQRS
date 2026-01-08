
namespace Mojo.Application.Features.Interventions.Handler.Command
{
    internal class CreateInterventionHandler : IRequestHandler<CreateInterventionCommand, Unit>
    {
        private readonly IInterventionRepository repository;
        private readonly IMapper mapper;

        public CreateInterventionHandler(IInterventionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
        {
            var intervention = mapper.Map<Mojo.Domain.Entities.Intervention>(request.dto);

            await repository.CreateAsync(intervention);

            return Unit.Value;
        }
    }
}
