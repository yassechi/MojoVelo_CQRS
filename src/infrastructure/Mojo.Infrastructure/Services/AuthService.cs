using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Contracts.Identity;
using Mojo.Domain.Entities;
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
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<JwtSettings> jwtSettings,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            try
            {
                _logger.LogInformation("Tentative de connexion pour l'email: {Email}", request.Email);

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    _logger.LogWarning("Utilisateur non trouvé pour l'email: {Email}", request.Email);
                    throw new UnauthorizedAccessException("Email ou mot de passe incorrect.");
                }

                _logger.LogDebug("Utilisateur trouvé: {UserName} (ID: {UserId})", user.UserName, user.Id);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Échec de connexion - mot de passe incorrect pour: {Email}", request.Email);
                    throw new UnauthorizedAccessException("Email ou mot de passe incorrect.");
                }

                _logger.LogInformation("Connexion réussie pour: {Email}", request.Email);

                var token = GenerateJwtToken(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Token = token
                };
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la connexion pour l'email: {Email}", request.Email);
                throw;
            }
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            try
            {
                _logger.LogInformation("Tentative d'inscription pour l'email: {Email}", request.Email);

                // Vérifier si l'email existe déjà
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("Tentative d'inscription avec un email déjà existant: {Email}", request.Email);
                    throw new InvalidOperationException("Un utilisateur avec cet email existe déjà.");
                }

                // Vérifier si le nom d'utilisateur existe déjà
                var existingUserName = await _userManager.FindByNameAsync(request.UserName);
                if (existingUserName != null)
                {
                    _logger.LogWarning("Tentative d'inscription avec un nom d'utilisateur déjà existant: {UserName}", request.UserName);
                    throw new InvalidOperationException("Ce nom d'utilisateur est déjà pris.");
                }

                // Créer le nouvel utilisateur
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
                    OrganisationId = request.OrganisationId
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    _logger.LogError("Échec de l'inscription pour l'email: {Email}. Erreurs: {Errors}",
                        request.Email,
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Échec de l'inscription: {errors}");
                }

                _logger.LogInformation("Inscription réussie pour l'email: {Email}", request.Email);

                // Générer le token JWT
                var token = GenerateJwtToken(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Token = token
                };
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'inscription pour l'email: {Email}", request.Email);
                throw;
            }
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordRequest request)
        {
            try
            {
                _logger.LogInformation("Tentative de changement de mot de passe pour l'utilisateur: {UserId}", userId);

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("Utilisateur non trouvé: {UserId}", userId);
                    throw new InvalidOperationException("Utilisateur non trouvé.");
                }

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Échec du changement de mot de passe pour: {UserId}. Erreurs: {Errors}",
                        userId,
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Échec du changement de mot de passe: {errors}");
                }

                _logger.LogInformation("Mot de passe changé avec succès pour: {UserId}", userId);
                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du changement de mot de passe pour: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                _logger.LogInformation("Demande de réinitialisation de mot de passe pour: {Email}", request.Email);

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    _logger.LogWarning("Demande de réinitialisation pour un email inexistant: {Email}", request.Email);
                    return true;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                _logger.LogInformation("Token de réinitialisation généré pour: {Email}", request.Email);
                _logger.LogDebug("Token: {Token}", token);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération du token de réinitialisation pour: {Email}", request.Email);
                throw;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                _logger.LogInformation("Tentative de réinitialisation de mot de passe pour: {Email}", request.Email);

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    _logger.LogWarning("Utilisateur non trouvé pour: {Email}", request.Email);
                    throw new InvalidOperationException("Utilisateur non trouvé.");
                }

                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Échec de la réinitialisation pour: {Email}. Erreurs: {Errors}",
                        request.Email,
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Échec de la réinitialisation: {errors}");
                }

                _logger.LogInformation("Mot de passe réinitialisé avec succès pour: {Email}", request.Email);
                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la réinitialisation du mot de passe pour: {Email}", request.Email);
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                _logger.LogDebug("Génération du token JWT pour l'utilisateur: {UserId}", user.Id);

                if (string.IsNullOrEmpty(_jwtSettings.Key))
                {
                    throw new InvalidOperationException("JWT Key est vide ou null dans appsettings.json");
                }

                if (_jwtSettings.Key.Length < 32)
                {
                    throw new InvalidOperationException($"JWT Key trop courte. Longueur actuelle: {_jwtSettings.Key.Length}, minimum: 32");
                }

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

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogDebug("Token JWT généré avec succès (longueur: {Length})", tokenString.Length);

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération du token JWT pour l'utilisateur: {UserId}", user.Id);
                throw;
            }
        }
    }
}