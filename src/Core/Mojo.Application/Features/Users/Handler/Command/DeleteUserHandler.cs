using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var user = await _repository.GetUserByStringId(request.Id);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Echec de la suppression de l'utilisateur.";
                response.Errors.Add($"Aucun utilisateur trouvé avec l'Id {request.Id}.");
                return response;
            }

            await _repository.DeleteByStringId(request.Id);

            response.Success = true;
            response.Message = "L'utilisateur a été supprimé avec succès.";
            response.StrId = request.Id;
            return response;
        }
    }
}