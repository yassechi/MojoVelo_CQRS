using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Discussions.Handler.Command
{
    public class DeleteDiscussionHandler : IRequestHandler<DeleteDiscussionCommand, BaseResponse>
    {
        private readonly IDiscussionRepository _repository;
        private readonly IMapper _mapper;

        public DeleteDiscussionHandler(IDiscussionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteDiscussionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            // Vérifier si l'entité existe
            var discussion = await _repository.GetByIdAsync(request.Id);

            if (discussion == null)
            {
                response.Succes = false;
                response.Message = "Echec de la suppression de la discussion.";
                response.Errors.Add($"Aucune discussion trouvée avec l'Id {request.Id}.");
                return response;
            }

            // Suppression
            await _repository.DeleteAsync(request.Id);

            // Succès
            response.Succes = true;
            response.Message = "La discussion a été supprimée avec succès.";
            response.Id = request.Id;

            return response;
        }
    }
}