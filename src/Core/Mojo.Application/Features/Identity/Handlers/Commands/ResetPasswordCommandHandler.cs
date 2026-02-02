using MediatR;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Features.Identity.Requests.Commands;
using Mojo.Application.Persistance.Contracts.Identity;

namespace Mojo.Application.Features.Identity.Handlers.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ResetPassword(new ResetPasswordRequest
            {
                Email = request.Email,
                Token = request.Token,
                NewPassword = request.NewPassword,
                ConfirmNewPassword = request.ConfirmNewPassword
            });
        }
    }
}