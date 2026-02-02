using MediatR;

namespace Mojo.Application.Features.Identity.Requests.Commands
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}