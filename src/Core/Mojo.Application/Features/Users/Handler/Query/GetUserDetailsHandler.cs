using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Exceptions;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Users.Handler.Query
{
    public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsRequest, UserDto>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public GetUserDetailsHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            var user = await repository.GetUserByStringId(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            return mapper.Map<UserDto>(user);
        }
    }
}