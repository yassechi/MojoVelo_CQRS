using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Request.Command
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public UserDto dto { get; set; }
    }
}
