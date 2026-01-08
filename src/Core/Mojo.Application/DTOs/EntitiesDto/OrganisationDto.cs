
namespace Mojo.Application.DTOs.EntitiesDto
{
    //public class OrganisationAddDto : BaseEntity<int>
    public class OrganisationDto : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public bool IsActif { get; set; }
    }

    //public class OrganisationUpdateDto : BaseEntity<int>
    //{
    //    public string Name { get; set; } = null!;
    //    public string Code { get; set; } = null!;
    //    public string Address { get; set; } = null!;
    //    public string ContactEmail { get; set; } = null!;
    //    public bool IsActif { get; set; }
    //}
}