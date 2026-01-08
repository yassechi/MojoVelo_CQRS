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
            // Correction : On passe les repositories requis au constructeur du validateur
            var validator = new ContratValidator(_veloRepository, _userRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false) throw new Exception("Validation du contrat échouée.");

            var oldContrat = await _repository.GetByIdAsync(request.dto.Id);
            if (oldContrat == null) throw new Exception("Contrat introuvable.");

            _mapper.Map(request.dto, oldContrat);

            // Note : vérifiez l'orthographe de UpdateAsync dans votre repository
            await _repository.UpadteAsync(oldContrat);

            return Unit.Value;
        }
    }
}