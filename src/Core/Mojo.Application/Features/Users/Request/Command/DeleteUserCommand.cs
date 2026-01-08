namespace Mojo.Application.Features.Users.Request.Command
{
    internal class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
