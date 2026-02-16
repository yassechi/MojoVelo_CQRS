using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;
using Mojo.Application.Features.MoisAmortissements.Request.Query;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Query
{
    public class GetMoisAmortissementsByContratHandler : IRequestHandler<GetMoisAmortissementsByContratRequest, List<MoisAmortissementDto>>
    {
        private readonly IContratRepository _contratRepository;
        private readonly IAmortissementRepository _amortissementRepository;
        private readonly IMoisAmortissementRepository _moisAmortissementRepository;
        private readonly IMapper _mapper;

        public GetMoisAmortissementsByContratHandler(
            IContratRepository contratRepository,
            IAmortissementRepository amortissementRepository,
            IMoisAmortissementRepository moisAmortissementRepository,
            IMapper mapper)
        {
            _contratRepository = contratRepository;
            _amortissementRepository = amortissementRepository;
            _moisAmortissementRepository = moisAmortissementRepository;
            _mapper = mapper;
        }

        public async Task<List<MoisAmortissementDto>> Handle(GetMoisAmortissementsByContratRequest request, CancellationToken cancellationToken)
        {
            if (request.ContratId <= 0)
            {
                return new List<MoisAmortissementDto>();
            }

            var contrat = await _contratRepository.GetByIdAsync(request.ContratId);
            if (contrat == null)
            {
                return new List<MoisAmortissementDto>();
            }

            var amortissements = await _amortissementRepository.GetByVeloIdAsync(contrat.VeloId);
            if (amortissements == null || amortissements.Count == 0)
            {
                return new List<MoisAmortissementDto>();
            }

            var amortissement = amortissements
                .OrderByDescending(a => a.IsActif)
                .ThenByDescending(a => a.DateDebut)
                .FirstOrDefault();

            if (amortissement == null)
            {
                return new List<MoisAmortissementDto>();
            }

            var items = await _moisAmortissementRepository.GetByAmortissementIdAsync(amortissement.Id);
            return _mapper.Map<List<MoisAmortissementDto>>(items);
        }
    }
}
