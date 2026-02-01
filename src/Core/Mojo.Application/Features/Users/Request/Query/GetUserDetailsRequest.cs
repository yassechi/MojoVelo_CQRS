using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Request.Query
{
    public class GetUserDetailsRequest : IRequest<UserDto>
    {
        public string Id { get; set; }
    }
}
