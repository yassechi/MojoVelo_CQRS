using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;
using Mojo.Application.Features.MoisAmortissements.Request.Query;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Query
{
    public class GetAllMoisAmortissementHandler : IRequestHandler<GetAllMoisAmortissementRequest, List<MoisAmortissementDto>>
    {
        private readonly IMoisAmortissementRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMoisAmortissementHandler(IMoisAmortissementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MoisAmortissementDto>> Handle(GetAllMoisAmortissementRequest request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync();
            var actifs = items.Where(m => m.IsActif).ToList();
            return _mapper.Map<List<MoisAmortissementDto>>(actifs);
        }
    }
}
