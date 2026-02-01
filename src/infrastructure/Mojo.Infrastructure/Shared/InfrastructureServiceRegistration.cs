using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Application.Persistance.Contracts.Identity;
using Mojo.Application.Persistance.Emails;
using Mojo.Infrastructure.Email;
using Mojo.Infrastructure.Services;  // ← CHANGÉ ICI

namespace Mojo.Infrastructure.Shared
{
    public static class InfrastructureServiceRegistration
    {
        public static void ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSender>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}