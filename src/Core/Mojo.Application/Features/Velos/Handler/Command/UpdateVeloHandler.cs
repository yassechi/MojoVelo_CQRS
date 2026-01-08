using Mojo.Application.DTOs.EntitiesDto.Velo.Validators;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class UpdateVeloHandler : IRequestHandler<UpdateVeloCommand, Unit>
    {
        private readonly IVeloRepository _repository;
        private readonly IMapper _mapper;

        public UpdateVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateVeloCommand request, CancellationToken cancellationToken)
        {
            var validator = new VeloValidator(_repository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                throw new Exception("La validation du vélo a échoué lors de la mise à jour.");
            }

            var oldVelo = await _repository.GetByIdAsync(request.dto.Id);

            if (oldVelo == null)
            {
                throw new Exception("Vélo introuvable.");
            }

            _mapper.Map(request.dto, oldVelo);

            await _repository.UpadteAsync(oldVelo);

            return Unit.Value;
        }
    }
}