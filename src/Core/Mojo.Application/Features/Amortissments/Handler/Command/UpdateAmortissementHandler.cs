using FluentValidation.Results;
using Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators;

namespace Mojo.Application.Features.Amortissements.Handler.Command
{
    public class UpdateAmortissementHandler : IRequestHandler<UpdateAmortissementCommand, BaseResponse>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public UpdateAmortissementHandler(IAmortissementRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<BaseResponse> Handle(UpdateAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new AmortissementValidator(_veloRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification de l'amortissement : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldAmortissement = await _repository.GetByIdAsync(request.dto.Id);
            if (oldAmortissement == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification de l'amortissement.";
                response.Errors.Add($"Aucun amortissement trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldAmortissement);
            await _repository.UpdateAsync(oldAmortissement);

            response.Success = true;
            response.Message = "L'amortissement a été modifié avec succès.";
            response.Id = oldAmortissement.Id;
            return response;
        }
    }
}