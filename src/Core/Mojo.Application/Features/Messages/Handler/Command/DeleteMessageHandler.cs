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
                response.Message = "Echec de la suppression du message.";
                response.Errors.Add($"Aucun message trouvé avec l'Id {request.Id}.");
                return response;
            }

            await _repository.DeleteAsync(request.Id);

            response.Success = true;
            response.Message = "Le message a été supprimé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}