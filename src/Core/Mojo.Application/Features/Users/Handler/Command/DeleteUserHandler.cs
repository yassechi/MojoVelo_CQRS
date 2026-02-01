using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public DeleteUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var user = await repository.GetByIdAsync(request.Id);
            if (user is null)
            {
                response.Succes = false;
                response.Message = $"L'utilsateur avec Id: {request.Id} n'existe pas !";
                return response;
            }
            await repository.DeleteAsync(request.Id);

            response.Succes = true;
            response.Message = $"L'utilsateur avec Id: {request.Id} est supprimé !";

            return response;
        }
    }
}
