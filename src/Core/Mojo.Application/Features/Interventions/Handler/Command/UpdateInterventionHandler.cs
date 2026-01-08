using Mojo.Application.DTOs.EntitiesDto.Intervention.Validators;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class UpdateInterventionHandler : IRequestHandler<UpdateInterventionCommand, Unit>
    {
        private readonly IInterventionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public UpdateInterventionHandler(IInterventionRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<Unit> Handle(UpdateInterventionCommand request, CancellationToken cancellationToken)
        {
            // On passe le repository au constructeur pour valider le VeloId
            var validator = new InterventionValidator(_veloRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (!res.IsValid) throw new Exception("Validation échouée pour la mise à jour de l'intervention.");

            var oldIntervention = await _repository.GetByIdAsync(request.dto.Id);
            if (oldIntervention == null) throw new Exception("Intervention introuvable.");

            _mapper.Map(request.dto, oldIntervention);

            await _repository.UpadteAsync(oldIntervention);

            return Unit.Value;
        }
    }
}