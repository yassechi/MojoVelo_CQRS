using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class UpdateContratHandler : IRequestHandler<UpdateContratCommand, Unit>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;

        public UpdateContratHandler(IContratRepository repository, IMapper mapper, IVeloRepository veloRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateContratCommand request, CancellationToken cancellationToken)
        {
            var validator = new ContratValidator(_veloRepository, _userRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false) throw new Exceptions.ValidationException(res);

            var oldContrat = await _repository.GetByIdAsync(request.dto.Id);
            if (oldContrat == null) throw new Exception("Contrat introuvable.");

            _mapper.Map(request.dto, oldContrat);

            await _repository.UpadteAsync(oldContrat);

            return Unit.Value;
        }
    }
}