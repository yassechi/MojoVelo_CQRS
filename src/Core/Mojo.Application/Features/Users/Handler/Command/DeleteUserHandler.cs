using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public DeleteUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.Id);
            if (user is null) throw new NotFoundException(nameof(User), request.Id);
            await repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
