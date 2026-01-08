using Mojo.Application.Features.Contrats.Request.Command;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    internal class UpdateContratHandler : IRequestHandler<UpdateContratCommand, Unit>
    {
        private readonly IContratRepository repository;
        private readonly IMapper mapper;

        public UpdateContratHandler(IContratRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContratCommand request, CancellationToken cancellationToken)
        {
            var oldContrat = await repository.GetByIdAsync(request.dto.Id);
            var updatedContrat = mapper.Map(request.dto, oldContrat);
            await repository.UpadteAsync(updatedContrat);
            return Unit.Value;
        }
    }
}
