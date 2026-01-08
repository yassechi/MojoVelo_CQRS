using Mojo.Application.DTOs.EntitiesDto.Velo.Validators;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class CreateVeloHandler : IRequestHandler<CreateVeloCommand, Unit>
    {
        private readonly IVeloRepository _repository;
        private readonly IMapper _mapper;

        public CreateVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateVeloCommand request, CancellationToken cancellationToken)
        {
            // Correction : Passer le repository au constructeur du VeloValidator
            var validator = new VeloValidator(_repository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                throw new Exception("La validation du vélo a échoué.");
            }

            var velo = _mapper.Map<Velo>(request.dto);

            await _repository.CreateAsync(velo);

            return Unit.Value;
        }
    }
}