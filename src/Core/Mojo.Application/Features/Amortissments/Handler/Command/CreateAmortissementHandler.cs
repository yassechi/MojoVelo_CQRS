using Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class CreateDemandeHandler : IRequestHandler<CreateAmortissementCommand, BaseResponse>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public CreateDemandeHandler(IAmortissementRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        public async Task<BaseResponse> Handle(CreateAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new AmortissementValidator(_veloRepository);
            var validationResult = await validator.ValidateAsync(request.amortissmentDto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création de l'amortissement : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var amortissement = _mapper.Map<Amortissement>(request.amortissmentDto);
            await _repository.CreateAsync(amortissement);

            response.Success = true;
            response.Message = "L'amortissement a été créé avec succès.";
            response.Id = amortissement.Id;
            return response;
        }
    }
}