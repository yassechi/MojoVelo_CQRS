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
            var discussion = await _repository.GetByIdAsync(request.Id);

            if (discussion == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation de la discussion.";
                response.Errors.Add($"Aucune discussion trouvée avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive la discussion
            discussion.IsActif = false;
            await _repository.UpdateAsync(discussion);

            response.Success = true;
            response.Message = "La discussion a été désactivée avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}