using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class DeleteVeloHandler : IRequestHandler<DeleteVeloCommand, Unit>
    {
        private readonly IVeloRepository repository;
        private readonly IMapper mapper;

        public DeleteVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteVeloCommand request, CancellationToken cancellationToken)
        {
            var velo = await repository.GetByIdAsync(request.Id);
            if (velo is null) throw new NotFoundException(nameof(Velo), request.Id);

            await repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
