namespace Mojo.Application.Features.Users.Request.Command
{
    public class DeleteUserCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
