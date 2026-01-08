using Mojo.Application.Features.Amortissments.Request.Query;

namespace Mojo.Application.Features.Amortissments.Handler.Query
{
    public class GetAmortissementDetailsHandler : IRequestHandler<GetAmortissementDetailsRequest, AmortissmentDto>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public GetAmortissementDetailsHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<AmortissmentDto> Handle(GetAmortissementDetailsRequest request, CancellationToken cancellationToken)
        {
            var amortissement = await repository.GetByIdAsync(request.Id);
            if (amortissement is null) throw new Exception();
            return  mapper.Map<AmortissmentDto>(amortissement);
        }
    }
}
