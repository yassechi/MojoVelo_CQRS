
namespace Mojo.Application.Features.Users.Handler.Command
{
    internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UpdateUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await repository.GetUserByStringId(request.dto.Id);
            var updatedUser = mapper.Map(request.dto, oldUser);
            await repository.UpadteAsync(updatedUser);
            return Unit.Value;
        }
    }
}
