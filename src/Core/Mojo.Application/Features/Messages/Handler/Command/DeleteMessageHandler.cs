using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, BaseResponse>
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public DeleteMessageHandler(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var message = await repository.GetByIdAsync(request.Id);
            if (message is null)
            {
                response.Succes = false;
                response.Message = $"Le message avec Id: {request.Id} n'existe pas !";
                return response;
            }
            await repository.DeleteAsync(request.Id);

            response.Succes = true;
            response.Message = $"Le message avec Id: {request.Id} est supprimé !";

            return response;
        }
    }
}
