namespace Mojo.Application.DTOs.EntitiesDto.User
{
    public class AdminUserListItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Role { get; set; }
        public bool IsActif { get; set; }
        public int OrganisationId { get; set; }
    }
}
