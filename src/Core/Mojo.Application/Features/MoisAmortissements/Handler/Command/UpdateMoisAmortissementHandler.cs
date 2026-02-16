using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement.Validators;
using Mojo.Application.Features.MoisAmortissements.Request.Command;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Command
{
    public class UpdateMoisAmortissementHandler : IRequestHandler<UpdateMoisAmortissementCommand, BaseResponse>
    {
        private readonly IMoisAmortissementRepository _repository;
        private readonly IAmortissementRepository _amortissementRepository;
        private readonly IMapper _mapper;

        public UpdateMoisAmortissementHandler(
            IMoisAmortissementRepository repository,
            IAmortissementRepository amortissementRepository,
            IMapper mapper)
        {
            _repository = repository;
            _amortissementRepository = amortissementRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UpdateMoisAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new MoisAmortissementValidator(_amortissementRepository, _repository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification du mois d'amortissement : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var existing = await _repository.GetByIdAsync(request.dto.Id);
            if (existing == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification du mois d'amortissement.";
                response.Errors.Add($"Aucun mois d'amortissement trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, existing);
            await _repository.UpdateAsync(existing);

            response.Success = true;
            response.Message = "Le mois d'amortissement a été modifié avec succès.";
            response.Id = existing.Id;
            return response;
        }
    }
}
