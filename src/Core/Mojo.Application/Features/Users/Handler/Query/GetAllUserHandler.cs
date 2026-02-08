using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Handler.Query
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, List<UserDto>>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public GetAllUserHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllAsync();

            // Filtrer uniquement les utilisateurs actifs
            var usersActifs = users.Where(u => u.IsActif).ToList();

            return mapper.Map<List<UserDto>>(usersActifs);
        }
    }
}