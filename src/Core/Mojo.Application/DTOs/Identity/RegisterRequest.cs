using System.ComponentModel.DataAnnotations;

namespace Mojo.Application.DTOs.Identity
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Le prénom est requis.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est requis.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation du mot de passe est requise.")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le rôle est requis.")]
        public int Role { get; set; }

        public float? TailleCm { get; set; }

        [Required(ErrorMessage = "L'organisation est requise.")]
        public int OrganisationId { get; set; }
    }
}