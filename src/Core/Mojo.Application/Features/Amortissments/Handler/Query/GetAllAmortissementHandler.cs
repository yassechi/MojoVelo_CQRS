using Mojo.Application.DTOs.EntitiesDto.Amortissement;
using Mojo.Application.Features.Amortissements.Request.Query;

namespace Mojo.Application.Features.Amortissments.Handler.Query
{
    public class GetAllDemandeHandler : IRequestHandler<GetAllAmortissementRequest, List<AmortissmentDto>>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public GetAllDemandeHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<AmortissmentDto>> Handle(GetAllAmortissementRequest request, CancellationToken cancellationToken)
        {
            var amortissements = await repository.GetAllAsync();

            // Filtrer uniquement les amortissements actifs
            var amortissementsActifs = amortissements.Where(a => a.IsActif).ToList();

            return mapper.Map<List<AmortissmentDto>>(amortissementsActifs);
        }
    }
}