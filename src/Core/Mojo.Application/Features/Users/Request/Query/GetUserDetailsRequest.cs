namespace Mojo.Application.Features.Users.Request.Query
{
    internal class GetUserDetailsRequest : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
