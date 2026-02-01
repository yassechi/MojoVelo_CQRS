using Mojo.Application.DTOs.EntitiesDto.Intervention;
using Mojo.Application.Exceptions;
using MediatR;
using AutoMapper;

namespace Mojo.Application.Features.Interventions.Handler.Query
{
    public class GetInterventionDetailsHandler : IRequestHandler<GetInterventionDetailsRequest, InterventionDto>
    {
        private readonly IInterventionRepository _repository;
        private readonly IMapper _mapper;

        public GetInterventionDetailsHandler(IInterventionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InterventionDto> Handle(GetInterventionDetailsRequest request, CancellationToken cancellationToken)
        {
            var intervention = await _repository.GetByIdAsync(request.Id);

            if (intervention == null)
            {
                throw new NotFoundException(nameof(Intervention), request.Id);
            }

            return _mapper.Map<InterventionDto>(intervention);
        }
    }
}