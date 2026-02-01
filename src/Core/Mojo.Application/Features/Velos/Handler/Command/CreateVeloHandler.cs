using Mojo.Application.DTOs.EntitiesDto.Velo.Validators;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class CreateVeloHandler : IRequestHandler<CreateVeloCommand, BaseResponse>
    {
        private readonly IVeloRepository _repository;
        private readonly IMapper _mapper;

        public CreateVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateVeloCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new VeloValidator(_repository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création du vélo : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var velo = _mapper.Map<Velo>(request.dto);
            await _repository.CreateAsync(velo);

            response.Succes = true;
            response.Message = "Le vélo a été créé avec succès.";
            response.Id = velo.Id;

            return response;
        }
    }
}