using Mojo.Application.DTOs.EntitiesDto.Velo;
using Mojo.Application.Exceptions;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Velos.Handler.Query
{
    public class GetVeloDetailsHandler : IRequestHandler<GetVeloDetailsRequest, VeloDto>
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

            if (velo == null || !velo.IsActif)
            {
                throw new NotFoundException(nameof(Velo), request.Id);
            }

            return mapper.Map<VeloDto>(velo);
        }
    }
}