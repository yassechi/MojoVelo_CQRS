using Mojo.Application.DTOs.EntitiesDto.Velo.Validators;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class UpdateVeloHandler : IRequestHandler<UpdateVeloCommand, BaseResponse>
    {
        private readonly IVeloRepository _repository;
        private readonly IMapper _mapper;

        public UpdateVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UpdateVeloCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new VeloValidator(_repository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la modification du vélo : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldVelo = await _repository.GetByIdAsync(request.dto.Id);

            if (oldVelo == null)
            {
                response.Succes = false;
                response.Message = "Echec de la modification du vélo.";
                response.Errors.Add($"Aucun vélo trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldVelo);
            await _repository.UpadteAsync(oldVelo);

            response.Succes = true;
            response.Message = "Le vélo a été modifié avec succès.";
            response.Id = oldVelo.Id;

            return response;
        }
    }
}