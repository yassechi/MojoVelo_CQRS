using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Request.Command
{
    public class CreateUserCommand : IRequest<BaseResponse>
    {
        public UserDto dto { get; set; }
    }
}
