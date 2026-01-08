using System.ComponentModel.DataAnnotations;

namespace Mojo.Application.DTOs.EntitiesDto.User
{
    //public class UserAddDto : BaseEntity<string>
    public class UserDto : BaseEntity<string>
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LasttName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public int Role { get; set; }
        public float? TailleCm { get; set; }
        public bool IsActif { get; set; } = true;
        [Required]
        public int OrganisationId { get; set; }
    }

    //public class UserUpdateDto : BaseEntity<string>
    //{
    //    public string FirstName { get; set; } = null!;
    //    public string LasttName { get; set; } = null!;
    //    public string Email { get; set; } = null!;
    //    public string PhoneNumber { get; set; } = null!;
    //    public string Password { get; set; } = null!;
    //    public int Role { get; set; }
    //    public float TailleCm { get; set; }
    //    public bool IsActif { get; set; }
    //    public int OrganisationId { get; set; }
    //}
}