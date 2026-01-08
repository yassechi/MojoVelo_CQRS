
namespace Mojo.Application.Features.Velos.Handler.Command
{
    internal class UpdateVeloHandler : IRequestHandler<UpdateVeloCommand, Unit>
    {
        private readonly IVeloRepository repository;
        private readonly IMapper mapper;

        public UpdateVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateVeloCommand request, CancellationToken cancellationToken)
        {
            var oldVelo = await repository.GetByIdAsync(request.dto.Id);
            var updatedVelo = mapper.Map(request.dto, oldVelo);
            await repository.UpadteAsync(updatedVelo);
            return Unit.Value;
        }
    }
}
