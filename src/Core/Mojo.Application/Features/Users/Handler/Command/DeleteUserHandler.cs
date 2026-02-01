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

            // Vérifier si l'entité existe (utiliser GetUserByStringId pour les ID string)
            var user = await _repository.GetUserByStringId(request.Id);

            if (user == null)
            {
                response.Succes = false;
                response.Message = "Echec de la suppression de l'utilisateur.";
                response.Errors.Add($"Aucun utilisateur trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Suppression
            await _repository.DeleteByStringId(request.Id);

            // Succès
            response.Succes = true;
            response.Message = "L'utilisateur a été supprimé avec succès.";
            response.StrId = request.Id;

            return response;
        }
    }
}