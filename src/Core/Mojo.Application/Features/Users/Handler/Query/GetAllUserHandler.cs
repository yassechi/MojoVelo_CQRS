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
            return mapper.Map<List<UserDto>>(users);
        }
    }
}
