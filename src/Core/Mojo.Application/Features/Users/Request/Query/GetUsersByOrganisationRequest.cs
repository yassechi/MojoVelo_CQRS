using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Request.Query
{
    public class GetUsersByOrganisationRequest : IRequest<List<UserDto>>
    {
        public int OrganisationId { get; set; }
        public int? Role { get; set; }
    }
}
