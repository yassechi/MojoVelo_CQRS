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
                response.Message = "Echec de la désactivation de l'utilisateur.";
                response.Errors.Add($"Aucun utilisateur trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive l'utilisateur
            user.IsActif = false;
            await _repository.UpdateAsync(user);

            response.Success = true;
            response.Message = "L'utilisateur a été désactivé avec succès.";
            response.StrId = request.Id;
            return response;
        }
    }
}