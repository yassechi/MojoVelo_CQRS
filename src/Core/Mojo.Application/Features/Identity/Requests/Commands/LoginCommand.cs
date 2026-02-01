using MediatR;
using Mojo.Application.DTOs.Identity;

namespace Mojo.Application.Features.Identity.Requests.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}