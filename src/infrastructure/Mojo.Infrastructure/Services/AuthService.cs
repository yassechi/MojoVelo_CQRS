using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Contracts.Identity;
using Mojo.Application.Persistance.Emails;
using Mojo.Domain.Entities;
using Mojo.Persistence.DatabaseContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mojo.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly MDbContext _context;
        private readonly IEmailSender _emailSender;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<JwtSettings> jwtSettings,
            MDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Email ou mot de passe incorrect.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Email ou mot de passe incorrect.");
            }

            return BuildAuthResponse(user);
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            var emailDomain = "@" + request.Email.Split('@')[1];
            var organisation = await _context.Organisations
                .FirstOrDefaultAsync(o => o.EmailAutorise == emailDomain && o.IsActif);

            if (organisation == null)
            {
                throw new InvalidOperationException(
                    $"Aucune organisation n'autorise les inscriptions avec le domaine {emailDomain}.");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role,
                TailleCm = request.TailleCm ?? 0,
                IsActif = true,
                OrganisationId = organisation.Id
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Échec de l'inscription.");
            }

            return BuildAuthResponse(user);
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("Utilisateur non trouvé.");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Échec du changement de mot de passe.");
            }

            return true;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return true;
            }

            // construit le lien de réinitialisation dans le corp de l email 
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = "http://localhost:4200/reset-password?token=" + token + "&email=" + user.Email;

            var emailMessage = new EmailMessage
            {
                To = user.Email,
                Subject = "Réinitialisation de votre mot de passe - MojoVelo",
                Body = $"Lien de réinitialisation : {resetLink}"
            };

            await _emailSender.SendEmail(emailMessage);
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Utilisateur non trouvé.");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Échec de la réinitialisation.");
            }

            return true;
        }

        private AuthResponse BuildAuthResponse(User user)
        {
            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                Token = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("uid", user.Id ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
