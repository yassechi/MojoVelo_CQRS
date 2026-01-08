
namespace Mojo.Application.Features.Velos.Handler.Query
{
    internal class GetAllVeloHandler : IRequestHandler<GetAllVeloRequest, List<VeloDto>>
    {
        private readonly IVeloRepository repository;
        private readonly IMapper mapper;

        public GetAllVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<VeloDto>> Handle(GetAllVeloRequest request, CancellationToken cancellationToken)
        {
            var velos = await repository.GetAllAsync();
            return mapper.Map<List<VeloDto>>(velos);
        }
    }
}
