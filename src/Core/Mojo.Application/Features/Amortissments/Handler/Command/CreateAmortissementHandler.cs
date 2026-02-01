using Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class CreateAmortissementHandler : IRequestHandler<CreateAmortissementCommand, BaseResponse>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;

        public CreateAmortissementHandler(IAmortissementRepository repository, IMapper mapper, IVeloRepository veloRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
        }

        async Task<BaseResponse> IRequestHandler<CreateAmortissementCommand, BaseResponse>.Handle(CreateAmortissementCommand request, CancellationToken cancellationToken)

        {
            var response = new BaseResponse();
            var validator = new AmortissementValidator(_veloRepository, _repository);
            var res = await validator.ValidateAsync(request.AmortissmentDto);

            if (res.IsValid == false)
            {
                response.Succes = false;
                response.Message = "Echec de creation !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            response.Succes = true;
            response.Message = "création ok.. ";
            response.Id = request.AmortissmentDto.Id;

            var amortissement = _mapper.Map<Amortissement>(request.AmortissmentDto);

            await _repository.CreateAsync(amortissement);

            return response;
        }


    }
}