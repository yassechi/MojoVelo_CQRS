using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratsByOrganisationHandler : IRequestHandler<GetContratsByOrganisationRequest, List<ContratDto>>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;

        public GetContratsByOrganisationHandler(IContratRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ContratDto>> Handle(GetContratsByOrganisationRequest request, CancellationToken cancellationToken)
        {
            if (request.OrganisationId <= 0)
            {
                return new List<ContratDto>();
            }

            var contrats = await _repository.GetByOrganisationIdAsync(request.OrganisationId);
            return _mapper.Map<List<ContratDto>>(contrats);
        }
    }
}
