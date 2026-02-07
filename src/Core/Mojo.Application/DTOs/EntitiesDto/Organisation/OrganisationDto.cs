namespace Mojo.Application.DTOs.EntitiesDto.Organisation
{
    public class OrganisationDto : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string EmailAutorise { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public bool IsActif { get; set; }
        public string IdContact { get; set; } = null!;  // ? Ajoutez cette ligne
    }
}