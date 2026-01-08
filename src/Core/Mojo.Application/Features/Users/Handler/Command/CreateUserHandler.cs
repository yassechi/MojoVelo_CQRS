
namespace Mojo.Application.Features.Users.Handler.Command
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public CreateUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request.dto);

            await repository.CreateAsync(user);

            return Unit.Value;
        }
    }
}
