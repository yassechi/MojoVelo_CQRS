using MediatR;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Features.Identity.Requests.Commands;
using Mojo.Application.Persistance.Contracts.Identity;

namespace Mojo.Application.Features.Identity.Handlers.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // On appelle le service d'authentification qu'on implémentera dans Persistence
        return await _authService.Login(request.Email, request.Password);
    }
}