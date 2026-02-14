using MediatR;
using Mojo.API.Attributes;
using Mojo.Application.DTOs.Identity;

namespace Mojo.Application.Features.Identity.Requests.Commands
{
    [PasswordConfirm]
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public int Role { get; set; }
        public float? TailleCm { get; set; }
        public int OrganisationId { get; set; }
    }
}