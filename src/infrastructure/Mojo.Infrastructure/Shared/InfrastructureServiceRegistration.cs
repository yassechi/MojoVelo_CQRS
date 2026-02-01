using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Mojo.Infrastructure.Email;
using Mojo.Application.Persistance.Emails;

namespace Mojo.Infrastructure.Shared
{
    public static class InfrastructureServiceRegistration
    {
        public static void ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSender>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
} 
