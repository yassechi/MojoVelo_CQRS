using Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class CreateAmortissementHandler : IRequestHandler<CreateAmortissementCommand, Unit>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public CreateAmortissementHandler(IAmortissementRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<Unit> Handle(CreateAmortissementCommand request, CancellationToken cancellationToken)
        {
            var validator = new AmortissementValidator(_veloRepository);

            var res = await validator.ValidateAsync(request.amortissmentDto, cancellationToken);

            if (res.IsValid == false)
            {
                throw new Exception("La validation de l'amortissement a échoué.");
            }

            var amortissement = _mapper.Map<Amortissement>(request.amortissmentDto);

            await _repository.CreateAsync(amortissement);

            return Unit.Value;
        }
    }
}