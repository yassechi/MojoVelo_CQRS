
namespace Mojo.Application.Features.Velos.Handler.Query
{
    internal class GetVeloDetailsHandler : IRequestHandler<GetVeloDetailsRequest, VeloDto>
    {
        private readonly IVeloRepository repository;
        private readonly IMapper mapper;

        public GetVeloDetailsHandler(IVeloRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<VeloDto> Handle(GetVeloDetailsRequest request, CancellationToken cancellationToken)
        {
            var velo = await repository.GetByIdAsync(request.Id);

            if (velo == null)
            {
                throw new Exception("Velo non trouvé");
            }

            return mapper.Map<VeloDto>(velo);
        }
    }
}
