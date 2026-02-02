using MediatR;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Features.Identity.Requests.Commands;
using Mojo.Application.Persistance.Contracts.Identity;

namespace Mojo.Application.Features.Identity.Handlers.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ForgotPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ForgotPassword(new ForgotPasswordRequest
            {
                Email = request.Email
            });
        }
    }
}