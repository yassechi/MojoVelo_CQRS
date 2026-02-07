using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Application.Features.Amortissments.Handler.Command;
using Mojo.Application.MappingProfiles;

namespace Mojo.API.Dependencies
{
    public static class ApplicationServiceRegistration
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(CreateDemandeHandler));
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }
    }
}

//services.AddValidatorsFromAssembly(assembly);