using Mojo.Application.Features.Amortissments.Request.Command;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class CreateAmortissementHandler : IRequestHandler<CreateAmortissementCommand, Unit>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public CreateAmortissementHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(CreateAmortissementCommand request, CancellationToken cancellationToken)
        {
            var amortissement = mapper.Map<Amortissement>(request.amortissmentDto);

            await repository.CreateAsync(amortissement);
            return Unit.Value;
        }
    }
}
