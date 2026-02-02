using MediatR;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Features.Identity.Requests.Commands;
using Mojo.Application.Persistance.Contracts.Identity;

namespace Mojo.Application.Features.Identity.Handlers.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Register(new RegisterRequest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Role = request.Role,
                TailleCm = request.TailleCm,
                OrganisationId = request.OrganisationId
            });
        }
    }
}