using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, BaseResponse>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public DeleteMessageHandler(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var message = await _repository.GetByIdAsync(request.Id);

            if (message == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation du message.";
                response.Errors.Add($"Aucun message trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive le message
            message.IsActif = false;
            await _repository.UpdateAsync(message);

            response.Success = true;
            response.Message = "Le message a été désactivé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}