using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mojo.Application.DTOs.Identity;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Contracts.Identity;
using Mojo.Application.Persistance.Emails;  // ← Ajouté
using Mojo.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mojo.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Mojo.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;
        private readonly MDbContext _context;
        private readonly IEmailSender _emailSender;  // ← Ajouté

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<JwtSettings> jwtSettings,
            ILogger<AuthService> logger,
            MDbContext context,
            IEmailSender emailSender)  // ← Ajouté
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
            _context = context;
            _emailSender = emailSender;  // ← Ajouté
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            try
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
                _logger.LogError(ex, "Erreur lors de la connexion");
                throw;
            }
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            try
            {
                // 1. Extraire le domaine de l'email
                var emailDomain = "@" + request.Email.Split('@')[1];

                // 2. Vérifier qu'une organisation avec ce domaine existe
                var organisation = await _context.Organisations
                    .FirstOrDefaultAsync(o => o.EmailAutorise == emailDomain && o.IsActif);

                if (organisation == null)
                {
                    throw new InvalidOperationException($"Aucune organisation n'autorise les inscriptions avec le domaine {emailDomain}. Veuillez contacter votre administrateur.");
                }

                // 3. Vérifier si l'email existe déjà
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("Un utilisateur avec cet email existe déjà.");
                }

                // 4. Vérifier si le nom d'utilisateur existe déjà
                var existingUserName = await _userManager.FindByNameAsync(request.UserName);
                if (existingUserName != null)
                {
                    throw new InvalidOperationException("Ce nom d'utilisateur est déjà pris.");
                }

                // 5. Créer le nouvel utilisateur avec association automatique à l'organisation
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
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Échec de l'inscription: {errors}");
                }

                // 6. Générer le token JWT
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
                _logger.LogError(ex, "Erreur lors de l'inscription");
                throw;
            }
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new InvalidOperationException("Utilisateur non trouvé.");
                }

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Échec du changement de mot de passe: {errors}");
                }

                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du changement de mot de passe");
                throw;
            }
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    // Pour la sécurité, on ne révèle pas si l'email existe ou non
                    return true;
                }

                // Générer le token de réinitialisation
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Créer le lien de réinitialisation (à adapter selon votre frontend)
                var resetLink = $"http://localhost:4200/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";

                // Envoyer l'email
                var emailMessage = new EmailMessage
                {
                    To = user.Email,
                    Subject = "Réinitialisation de votre mot de passe - MojoVelo",
                    Body = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h2>Réinitialisation de mot de passe</h2>
                            <p>Bonjour {user.FirstName} {user.LastName},</p>
                            <p>Vous avez demandé à réinitialiser votre mot de passe.</p>
                            <p>Cliquez sur le lien ci-dessous pour réinitialiser votre mot de passe :</p>
                            <p><a href='{resetLink}' style='background-color: #1e3a8a; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Réinitialiser mon mot de passe</a></p>
                            <p>Ce lien expire dans 24 heures.</p>
                            <p>Si vous n'avez pas demandé cette réinitialisation, ignorez cet email.</p>
                            <br/>
                            <p>Cordialement,<br/>L'équipe MojoVelo</p>
                        </body>
                        </html>
                    "
                };

                await _emailSender.SendEmail(emailMessage);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'envoi de l'email de réinitialisation");
                throw;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new InvalidOperationException("Utilisateur non trouvé.");
                }

                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Échec de la réinitialisation: {errors}");
                }

                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la réinitialisation du mot de passe");
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
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

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération du token JWT");
                throw;
            }
        }
    }
}