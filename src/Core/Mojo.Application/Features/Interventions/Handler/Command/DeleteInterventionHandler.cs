namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class DeleteInterventionHandler : IRequestHandler<DeleteInterventionCommand, Unit>
    {
        private readonly IInterventionRepository repository;
        private readonly IMapper mapper;

        public DeleteInterventionHandler(IInterventionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteInterventionCommand request, CancellationToken cancellationToken)
        {
            var intervention = await repository.GetByIdAsync(request.Id);

            if (intervention != null)
            {
                await repository.DeleteAsync(request.Id);
            }

            return Unit.Value;
        }
    }
}
