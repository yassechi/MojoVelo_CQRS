using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class DeleteVeloHandler : IRequestHandler<DeleteVeloCommand, BaseResponse>
    {
        private readonly IVeloRepository _repository;
        private readonly IMapper _mapper;

        public DeleteVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteVeloCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            // Vérifier si l'entité existe
            var velo = await _repository.GetByIdAsync(request.Id);

            if (velo == null)
            {
                response.Succes = false;
                response.Message = "Echec de la suppression du vélo.";
                response.Errors.Add($"Aucun vélo trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Suppression
            await _repository.DeleteAsync(request.Id);

            // Succès
            response.Succes = true;
            response.Message = "Le vélo a été supprimé avec succès.";
            response.Id = request.Id;

            return response;
        }
    }
}