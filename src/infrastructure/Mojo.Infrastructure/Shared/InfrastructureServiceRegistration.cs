using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Application.Model;  // ← AJOUTÉ pour EmailSettings
using Mojo.Application.Persistance.Contracts.Identity;
using Mojo.Application.Persistance.Emails;
using Mojo.Infrastructure.Email;
using Mojo.Infrastructure.Services;

namespace Mojo.Infrastructure.Shared
{
    public static class InfrastructureServiceRegistration
    {
        public static void ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // ⬇️ CORRECTION ICI : EmailSettings au lieu de EmailSender ⬇️
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}