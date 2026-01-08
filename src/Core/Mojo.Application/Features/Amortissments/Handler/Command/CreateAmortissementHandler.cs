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
            var validator = new AmortissementValidator(_veloRepository);
            var res = await validator.ValidateAsync(request.amortissmentDto, cancellationToken);

            if (res.IsValid == false)
            {
                response.Succes = false;
                response.Message = "Echec de creation !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
                throw new Exception("La validation de l'amortissement a échoué.");
            }

            response.Succes = true;
            response.Message = "création ok.. ";
            response.Id = request.amortissmentDto.Id;

            var amortissement = _mapper.Map<Amortissement>(request.amortissmentDto);

            await _repository.CreateAsync(amortissement);

            return response;
        }


    }
}