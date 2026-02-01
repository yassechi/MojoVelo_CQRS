using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.Features.Amortissments.Handler.Query
{
    public class GetAllAmortissementHandler : IRequestHandler<GetAllAmortissementRequest, List<AmortissmentDto>>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public GetAllAmortissementHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<AmortissmentDto>> Handle(GetAllAmortissementRequest request, CancellationToken cancellationToken)
        {
            var amortissements = await repository.GetAllAsync();
            return mapper.Map<List<AmortissmentDto>>(amortissements);
        }
    }
}
