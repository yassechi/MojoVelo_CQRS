using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Exceptions;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratDetailsHandler : IRequestHandler<GetContratDetailsRequest, ContratDto>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;

        public GetContratDetailsHandler(IContratRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ContratDto> Handle(GetContratDetailsRequest request, CancellationToken cancellationToken)
        {
            var contrat = await _repository.GetByIdAsync(request.Id);

            if (contrat == null || !contrat.IsActif)
            {
                throw new NotFoundException(nameof(Contrat), request.Id);
            }

            return _mapper.Map<ContratDto>(contrat);
        }
    }
}