using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;
using Mojo.Application.Features.MoisAmortissements.Request.Query;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Query
{
    public class GetMoisAmortissementsByAmortissementHandler : IRequestHandler<GetMoisAmortissementsByAmortissementRequest, List<MoisAmortissementDto>>
    {
        private readonly IMoisAmortissementRepository _repository;
        private readonly IMapper _mapper;

        public GetMoisAmortissementsByAmortissementHandler(IMoisAmortissementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MoisAmortissementDto>> Handle(GetMoisAmortissementsByAmortissementRequest request, CancellationToken cancellationToken)
        {
            if (request.AmortissementId <= 0)
            {
                return new List<MoisAmortissementDto>();
            }

            var items = await _repository.GetByAmortissementIdAsync(request.AmortissementId);
            return _mapper.Map<List<MoisAmortissementDto>>(items);
        }
    }
}
