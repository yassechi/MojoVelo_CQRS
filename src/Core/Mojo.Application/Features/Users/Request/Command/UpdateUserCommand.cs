namespace Mojo.Application.Features.Users.Request.Command
{
    internal class UpdateUserCommand : IRequest<Unit>
    {
        public UserDto dto { get; set; }
    }
}
