using Mojo.Application.DTOs.EntitiesDto.User;

namespace Mojo.Application.Features.Users.Request.Query
{
    public class GetUserListRequest : IRequest<List<AdminUserListItemDto>>
    {
        public int? Role { get; set; }
        public bool? IsActif { get; set; }
        public string? Search { get; set; }
        public int? OrganisationId { get; set; }
    }
}
