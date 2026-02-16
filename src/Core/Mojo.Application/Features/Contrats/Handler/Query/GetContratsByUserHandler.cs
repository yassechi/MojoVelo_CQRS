using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratsByUserHandler : IRequestHandler<GetContratsByUserRequest, List<ContratDto>>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;

        public GetContratsByUserHandler(IContratRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ContratDto>> Handle(GetContratsByUserRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return new List<ContratDto>();
            }

            var contrats = await _repository.GetByUserIdAsync(request.UserId);
            return _mapper.Map<List<ContratDto>>(contrats);
        }
    }
}
