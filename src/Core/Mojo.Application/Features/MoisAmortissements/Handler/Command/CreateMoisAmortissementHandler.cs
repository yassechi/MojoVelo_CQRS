using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement.Validators;
using Mojo.Application.Features.MoisAmortissements.Request.Command;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Command
{
    public class CreateMoisAmortissementHandler : IRequestHandler<CreateMoisAmortissementCommand, BaseResponse>
    {
        private readonly IMoisAmortissementRepository _repository;
        private readonly IAmortissementRepository _amortissementRepository;
        private readonly IMapper _mapper;

        public CreateMoisAmortissementHandler(
            IMoisAmortissementRepository repository,
            IAmortissementRepository amortissementRepository,
            IMapper mapper)
        {
            _repository = repository;
            _amortissementRepository = amortissementRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateMoisAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new MoisAmortissementValidator(_amortissementRepository, _repository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création du mois d'amortissement : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var mois = _mapper.Map<MoisAmortissement>(request.dto);
            await _repository.CreateAsync(mois);

            response.Success = true;
            response.Message = "Le mois d'amortissement a été créé avec succès.";
            response.Id = mois.Id;
            return response;
        }
    }
}
