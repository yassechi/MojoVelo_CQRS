using Mojo.Application.DTOs.EntitiesDto.Intervention.Validators;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class CreateInterventionHandler : IRequestHandler<CreateInterventionCommand, Unit>
    {
        private readonly IInterventionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public CreateInterventionHandler(IInterventionRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<Unit> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
        {
            var validator = new InterventionValidator(_veloRepository);
            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid) throw new Exception("Erreur de validation pour l'intervention.");

            var intervention = _mapper.Map<Mojo.Domain.Entities.Intervention>(request.dto);
            await _repository.CreateAsync(intervention);

            return Unit.Value;
        }
    }
}