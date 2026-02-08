using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class DeleteContratHandler : IRequestHandler<DeleteContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;

        public DeleteContratHandler(IContratRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var contrat = await _repository.GetByIdAsync(request.Id);

            if (contrat == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation du contrat.";
                response.Errors.Add($"Aucun contrat trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive le contrat
            contrat.IsActif = false;
            await _repository.UpdateAsync(contrat);

            response.Success = true;
            response.Message = "Le contrat a été désactivé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}