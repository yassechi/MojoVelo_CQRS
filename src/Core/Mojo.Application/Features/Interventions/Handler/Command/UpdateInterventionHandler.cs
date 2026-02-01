using Mojo.Application.DTOs.EntitiesDto.Intervention.Validators;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class UpdateInterventionHandler : IRequestHandler<UpdateInterventionCommand, BaseResponse>
    {
        private readonly IInterventionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public UpdateInterventionHandler(IInterventionRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<BaseResponse> Handle(UpdateInterventionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new InterventionValidator(_veloRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la modification de l'intervention : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldIntervention = await _repository.GetByIdAsync(request.dto.Id);

            if (oldIntervention == null)
            {
                response.Succes = false;
                response.Message = "Echec de la modification de l'intervention.";
                response.Errors.Add($"Aucune intervention trouvée avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldIntervention);
            await _repository.UpadteAsync(oldIntervention);

            response.Succes = true;
            response.Message = "L'intervention a été modifiée avec succès.";
            response.Id = oldIntervention.Id;

            return response;
        }
    }
}