using MediatR;

namespace Mojo.Application.Features.Identity.Requests.Commands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public string UserId { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}