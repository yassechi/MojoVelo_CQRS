using Mojo.Application.Features.Amortissments.Request.Command;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class DeleteAmortissementHandler : IRequestHandler<DeleteAmortissementCommand, BaseResponse>
    {
        private readonly IAmortissementRepository _repository;
        private readonly IMapper _mapper;

        public DeleteAmortissementHandler(IAmortissementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            // Vérifier si l'entité existe
            var amortissement = await _repository.GetByIdAsync(request.Id);

            if (amortissement == null)
            {
                response.Succes = false;
                response.Message = "Echec de la suppression de l'amortissement.";
                response.Errors.Add($"Aucun amortissement trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Suppression
            await _repository.DeleteAsync(request.Id);

            // Succès
            response.Succes = true;
            response.Message = "L'amortissement a été supprimé avec succès.";
            response.Id = request.Id;

            return response;
        }
    }
}