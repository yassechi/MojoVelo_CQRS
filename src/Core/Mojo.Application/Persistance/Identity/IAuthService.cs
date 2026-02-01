using Mojo.Application.DTOs.Identity;
namespace Mojo.Application.Persistance.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(string email, string password);
}