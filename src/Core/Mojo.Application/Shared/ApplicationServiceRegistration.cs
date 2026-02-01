using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Application.MappingProfiles;

namespace Mojo.Application.Shared
{
    public static class ApplicationServiceRegistration
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);
        }
    }
}