using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.DTOs
{
    public class UserAddDto
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LasttName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public int Role { get; set; }
        public float? TailleCm { get; set; }
        public bool IsActif { get; set; } = true;
        [Required]
        public int OrganisationId { get; set; }
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }
        public int OrganisationId { get; set; }
    }

    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

}