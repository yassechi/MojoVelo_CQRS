using Mojo.Application.DTOs.Identity;

namespace Mojo.Application.Persistance.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginRequest request);
        Task<AuthResponse> Register(RegisterRequest request);
        Task<bool> ChangePassword(string userId, ChangePasswordRequest request);
        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}