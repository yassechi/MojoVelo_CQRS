
namespace Mojo.Application.Features.Velos.Handler.Command
{
    internal class CreateVeloHandler : IRequestHandler<CreateVeloCommand, Unit>
    {
        private readonly IVeloRepository repository;
        private readonly IMapper mapper;

        public CreateVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateVeloCommand request, CancellationToken cancellationToken)
        {
            var velo = mapper.Map<Velo>(request.dto);

            await repository.CreateAsync(velo);

            return Unit.Value;
        }
    }
}
