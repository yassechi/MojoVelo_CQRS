using Mojo.Application.Features.MoisAmortissements.Request.Command;

namespace Mojo.Application.Features.MoisAmortissements.Handler.Command
{
    public class DeleteMoisAmortissementHandler : IRequestHandler<DeleteMoisAmortissementCommand, BaseResponse>
    {
        private readonly IMoisAmortissementRepository _repository;

        public DeleteMoisAmortissementHandler(IMoisAmortissementRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse> Handle(DeleteMoisAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                response.Success = false;
                response.Message = "Echec de la suppression du mois d'amortissement.";
                response.Errors.Add($"Aucun mois d'amortissement trouvé avec l'Id {request.Id}.");
                return response;
            }

            await _repository.DeleteAsync(request.Id);

            response.Success = true;
            response.Message = "Le mois d'amortissement a été supprimé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}
