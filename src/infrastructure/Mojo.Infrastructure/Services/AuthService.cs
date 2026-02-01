using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Contracts.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mojo.Domain.Entities;

namespace Mojo.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            try
            {
                Console.WriteLine("=== DÉBUT LOGIN ===");
                Console.WriteLine($"Email reçu: {request.Email}");

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    Console.WriteLine("❌ Utilisateur non trouvé");
                    throw new UnauthorizedAccessException("Email ou mot de passe incorrect.");
                }

                Console.WriteLine($"✅ Utilisateur trouvé: {user.UserName}");
                Console.WriteLine($"User ID: {user.Id}");
                Console.WriteLine($"User Email: {user.Email}");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                    Console.WriteLine("❌ Mot de passe incorrect");
                    throw new UnauthorizedAccessException("Email ou mot de passe incorrect.");
                }

                Console.WriteLine("✅ Mot de passe correct");
                Console.WriteLine("🔑 Génération du token...");

                var token = GenerateJwtToken(user);

                Console.WriteLine("✅ Token généré avec succès !");
                Console.WriteLine("=== FIN LOGIN ===");

                return new AuthResponse
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERREUR GLOBALE: {ex.GetType().Name}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                Console.WriteLine("--- Génération JWT ---");

                // Vérifier les paramètres JWT
                Console.WriteLine($"JWT Key: {(_jwtSettings.Key != null ? $"Présent ({_jwtSettings.Key.Length} caractères)" : "NULL ❌")}");
                Console.WriteLine($"JWT Issuer: {_jwtSettings.Issuer ?? "NULL ❌"}");
                Console.WriteLine($"JWT Audience: {_jwtSettings.Audience ?? "NULL ❌"}");
                Console.WriteLine($"JWT DurationInMinutes: {_jwtSettings.DurationInMinutes}");

                if (string.IsNullOrEmpty(_jwtSettings.Key))
                {
                    throw new InvalidOperationException("JWT Key est vide ou null dans appsettings.json !");
                }

                if (_jwtSettings.Key.Length < 32)
                {
                    throw new InvalidOperationException($"JWT Key trop courte ! Longueur actuelle: {_jwtSettings.Key.Length}, minimum requis: 32");
                }

                // Vérifier les données utilisateur
                Console.WriteLine($"User.UserName: {user.UserName ?? "NULL"}");
                Console.WriteLine($"User.Email: {user.Email ?? "NULL"}");
                Console.WriteLine($"User.Id: {user.Id ?? "NULL"}");
                Console.WriteLine($"User.Role: {user.Role}");
                Console.WriteLine($"User.Role (int): {(int)user.Role}");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                Console.WriteLine("✅ SecurityKey créée");

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                Console.WriteLine("✅ Credentials créées");

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                    new Claim("uid", user.Id ?? string.Empty),
                    new Claim(ClaimTypes.Role, ((int)user.Role).ToString())
                };
                Console.WriteLine($"✅ Claims créées ({claims.Length} claims)");

                var expiration = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);
                Console.WriteLine($"Expiration: {expiration}");

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials);

                Console.WriteLine("✅ JwtSecurityToken créé");

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                Console.WriteLine($"✅ Token écrit (longueur: {tokenString.Length} caractères)");

                return tokenString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERREUR dans GenerateJwtToken: {ex.GetType().Name}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }

                throw;
            }
        }
    }
}