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
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création de l'intervention : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var intervention = _mapper.Map<Mojo.Domain.Entities.Intervention>(request.dto);
            await _repository.CreateAsync(intervention);

            response.Succes = true;
            response.Message = "L'intervention a été créée avec succès.";
            response.Id = intervention.Id;

            return response;
        }
    }
}