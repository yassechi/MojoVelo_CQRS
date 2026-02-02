using MediatR;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Features.Identity.Requests.Commands;
using Mojo.Application.Persistance.Contracts.Identity;

namespace Mojo.Application.Features.Identity.Handlers.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ChangePasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ChangePassword(request.UserId, new ChangePasswordRequest
            {
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword,
                ConfirmNewPassword = request.ConfirmNewPassword
            });
        }
    }
}