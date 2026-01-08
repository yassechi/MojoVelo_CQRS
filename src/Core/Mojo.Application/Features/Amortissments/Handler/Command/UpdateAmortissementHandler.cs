using Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class UpdateAmortissementHandler : IRequestHandler<UpdateAmortissementCommand, Unit>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public UpdateAmortissementHandler(IAmortissementRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<Unit> Handle(UpdateAmortissementCommand request, CancellationToken cancellationToken)
        {
            var validator = new AmortissementValidator(_veloRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false) throw new Exception("Erreur de validation pour l'amortissement.");

            var oldAmortissement = await _repository.GetByIdAsync(request.dto.Id);
            if (oldAmortissement == null) throw new Exception("Amortissement introuvable.");

            _mapper.Map(request.dto, oldAmortissement);
            await _repository.UpadteAsync(oldAmortissement);

            return Unit.Value;
        }
    }
}