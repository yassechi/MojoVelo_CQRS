using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class CreateContratHandler : IRequestHandler<CreateContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;

        public CreateContratHandler(IContratRepository repository, IMapper mapper, IVeloRepository veloRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(CreateContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new ContratValidator(_veloRepository, _userRepository);
            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (res.IsValid == false)
            {
                response.Succes = false;
                response.Message = "Echec de creation du contrat !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }

            response.Succes = true;
            response.Message = "Creation avec succès..";
            response.Id = request.dto.Id;

            var contrat = _mapper.Map<Mojo.Domain.Entities.Contrat>(request.dto);

            await _repository.CreateAsync(contrat);

            return response;
        }
    }
}