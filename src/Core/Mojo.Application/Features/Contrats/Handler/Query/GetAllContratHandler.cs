using Mojo.Application.DTOs.EntitiesDto.Contrat;

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

            // Filtrer uniquement les contrats actifs
            var contratsActifs = contrats.Where(c => c.IsActif).ToList();

            return mapper.Map<List<ContratDto>>(contratsActifs);
        }
    }
}