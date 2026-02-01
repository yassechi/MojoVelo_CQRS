
namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class UpdateContratHandler : IRequestHandler<UpdateContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;

        public UpdateContratHandler(IContratRepository repository, IMapper mapper, IVeloRepository veloRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(UpdateContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new ContratValidator(_veloRepository, _userRepository, _repository);
            var res = await validator.ValidateAsync(request.dto, cancellationToken);
   

            // SI PAS VALIDE : On lance l'exception, ce qui ARRÊTE le code ici.
            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de creation du contrat !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            // Ce code ne s'exécutera JAMAIS si le rôle n'est pas Négociateur
            var oldContrat = await _repository.GetByIdAsync(request.dto.Id);
            if (oldContrat == null) throw new Exception("Contrat introuvable.");

            _mapper.Map(request.dto, oldContrat);
            await _repository.UpadteAsync(oldContrat);


            response.Succes = true;
            response.Message = "Modification avec succès..";
            response.Id = request.dto.Id;

            return response;
        }
    }
}