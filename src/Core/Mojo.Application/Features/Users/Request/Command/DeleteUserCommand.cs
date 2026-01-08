namespace Mojo.Application.Features.Users.Request.Command
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
