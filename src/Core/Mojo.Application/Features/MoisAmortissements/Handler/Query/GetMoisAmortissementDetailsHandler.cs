using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;
using Mojo.Application.Features.MoisAmortissements.Request.Query;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Query
{
    public class GetMoisAmortissementDetailsHandler : IRequestHandler<GetMoisAmortissementDetailsRequest, MoisAmortissementDto>
    {
        private readonly IMoisAmortissementRepository _repository;
        private readonly IMapper _mapper;

        public GetMoisAmortissementDetailsHandler(IMoisAmortissementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MoisAmortissementDto> Handle(GetMoisAmortissementDetailsRequest request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(request.Id);

            if (item == null || !item.IsActif)
            {
                throw new NotFoundException(nameof(MoisAmortissement), request.Id);
            }

            return _mapper.Map<MoisAmortissementDto>(item);
        }
    }
}
