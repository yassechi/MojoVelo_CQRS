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
            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création du vélo !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }
            response.Succes = true;
            response.Message = "Création du vélo avec succès..";
            response.Id = request.dto.Id;

            var velo = _mapper.Map<Velo>(request.dto);

            await _repository.CreateAsync(velo);

            return response;
        }
    }
}