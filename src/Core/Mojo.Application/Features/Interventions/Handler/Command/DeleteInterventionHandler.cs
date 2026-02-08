using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class DeleteInterventionHandler : IRequestHandler<DeleteInterventionCommand, BaseResponse>
    {
        private readonly IInterventionRepository _repository;
        private readonly IMapper _mapper;

        public DeleteInterventionHandler(IInterventionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteInterventionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var intervention = await _repository.GetByIdAsync(request.Id);

            if (intervention == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation de l'intervention.";
                response.Errors.Add($"Aucune intervention trouvée avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive l'intervention
            intervention.IsActif = false;
            await _repository.UpdateAsync(intervention);

            response.Success = true;
            response.Message = "L'intervention a été désactivée avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}