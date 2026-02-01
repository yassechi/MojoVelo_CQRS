 using Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators;

namespace Mojo.Application.Features.Amortissments.Handler.Command
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
            var validator = new AmortissementValidator(_veloRepository, _repository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false)
            {
                response.Succes = false;
                response.Message = "Echec de la modification !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldAmortissement = await _repository.GetByIdAsync(request.dto.Id);
            if (oldAmortissement == null) throw new Exception("Amortissement introuvable.");

            _mapper.Map(request.dto, oldAmortissement);
            await _repository.UpadteAsync(oldAmortissement);
            response.Succes = true;
            response.Message = "modification ok.. ";
            response.Id = request.dto.Id;

            return response;
        }
    }
}