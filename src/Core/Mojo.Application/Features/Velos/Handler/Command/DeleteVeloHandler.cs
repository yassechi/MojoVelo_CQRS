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
            var velo = await _repository.GetByIdAsync(request.Id);

            if (velo == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation du vélo.";
                response.Errors.Add($"Aucun vélo trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive le vélo
            velo.IsActif = false;
            await _repository.UpdateAsync(velo);

            response.Success = true;
            response.Message = "Le vélo a été désactivé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}