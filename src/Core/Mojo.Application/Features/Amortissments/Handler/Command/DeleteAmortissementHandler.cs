using Mojo.Application.Features.Amortissments.Request.Command;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class DeleteAmortissementHandler : IRequestHandler<DeleteAmortissementCommand, Unit>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public DeleteAmortissementHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteAmortissementCommand request, CancellationToken cancellationToken)
        {
            var amortissement = await repository.GetByIdAsync(request.Id);

            if (amortissement != null)
            {
                await repository.DeleteAsync(request.Id);
            }

            return Unit.Value;
        }
    }
}
