
namespace Mojo.Application.Features.Contrat.Handler.Query
{
    public class GetAllContratHandler : IRequestHandler<GetAllContratRequest, List<ContratDto>>
    {
        private readonly IContratRepository repository;
        private readonly IMapper mapper;

        public GetAllContratHandler(IContratRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ContratDto>> Handle(GetAllContratRequest request, CancellationToken cancellationToken)
        {
            var contrats = await repository.GetAllAsync();
            return mapper.Map<List<ContratDto>>(contrats);
        }
    }
}
        