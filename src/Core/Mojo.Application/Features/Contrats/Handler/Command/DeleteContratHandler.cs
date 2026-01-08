using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class DeleteContratHandler : IRequestHandler<DeleteContratCommand, Unit>
    {
        private readonly IContratRepository repository;
        private readonly IMapper mapper;

        public DeleteContratHandler(IContratRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteContratCommand request, CancellationToken cancellationToken)
        {
            var contrat = await repository.GetByIdAsync(request.Id);
            if (contrat is null) throw new NotFoundException(nameof(Mojo.Domain.Entities.Contrat), request.Id);
            await repository.DeleteAsync(request.Id);
            
            return Unit.Value;
        }
    }
}
