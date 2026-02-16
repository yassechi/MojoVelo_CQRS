using Mojo.Application.DTOs.EntitiesDto.Amortissement;
using Mojo.Application.Features.Amortissements.Request.Query;

namespace Mojo.Application.Features.Amortissments.Handler.Query
{
    public class GetAmortissementsByVeloHandler : IRequestHandler<GetAmortissementsByVeloRequest, List<AmortissmentDto>>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;

        public GetAmortissementsByVeloHandler(IAmortissementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AmortissmentDto>> Handle(GetAmortissementsByVeloRequest request, CancellationToken cancellationToken)
        {
            if (request.VeloId <= 0)
            {
                return new List<AmortissmentDto>();
            }

            var amortissements = await _repository.GetByVeloIdAsync(request.VeloId);
            return _mapper.Map<List<AmortissmentDto>>(amortissements);
        }
    }
}
