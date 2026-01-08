namespace Mojo.Application.Features.Users.Request.Command
{
    internal class CreateUserCommand : IRequest<Unit>
    {
        public UserDto dto { get; set; }
    }
}
