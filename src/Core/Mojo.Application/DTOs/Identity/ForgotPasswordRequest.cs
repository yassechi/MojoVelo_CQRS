using System.ComponentModel.DataAnnotations;

namespace Mojo.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        public string Email { get; set; } = string.Empty;
    }
}