using Mojo.Application.DTOs.EntitiesDto.Intervention.Validators;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class CreateInterventionHandler : IRequestHandler<CreateInterventionCommand, BaseResponse>
    {
        private readonly IInterventionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public CreateInterventionHandler(IInterventionRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<BaseResponse> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new InterventionValidator(_veloRepository);
            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la creation de l'intervention !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }

            response.Succes = true;
            response.Message = "=Creation de l'intervention avec Succès.. ";
            response.Id = request.dto.Id;

            var intervention = _mapper.Map<Mojo.Domain.Entities.Intervention>(request.dto);
            await _repository.CreateAsync(intervention);

            return response;
        }
    }
}