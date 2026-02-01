namespace Mojo.Application.Features.Users.Request.Command
{
    public class DeleteUserCommand : IRequest<BaseResponse>
    {
        public string Id { get; set; }
    }
}
