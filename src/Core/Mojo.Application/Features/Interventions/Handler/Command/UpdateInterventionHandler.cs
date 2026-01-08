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
            var validator = new InterventionValidator(_veloRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false) throw new Exceptions.ValidationException(res);

            var oldIntervention = await _repository.GetByIdAsync(request.dto.Id);
            if (oldIntervention == null) throw new Exception("Intervention introuvable.");

            _mapper.Map(request.dto, oldIntervention);

            await _repository.UpadteAsync(oldIntervention);

            return Unit.Value;
        }
    }
}