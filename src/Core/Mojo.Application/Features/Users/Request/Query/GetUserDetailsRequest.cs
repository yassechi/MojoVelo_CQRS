using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Request.Query
{
    public class GetUserDetailsRequest : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
