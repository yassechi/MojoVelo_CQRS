using Mojo.Application.Features.Amortissments.Request.Command;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class DeleteDemandeHandler : IRequestHandler<DeleteAmortissementCommand, BaseResponse>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;

        public DeleteDemandeHandler(IAmortissementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var amortissement = await _repository.GetByIdAsync(request.Id);

            if (amortissement == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation de l'amortissement.";
                response.Errors.Add($"Aucun amortissement trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive l'amortissement
            amortissement.IsActif = false;
            await _repository.UpdateAsync(amortissement);

            response.Success = true;
            response.Message = "L'amortissement a été désactivé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}