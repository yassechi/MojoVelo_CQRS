
using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;

namespace Mojo.Application.Shared
{
    public static class ApplicationServiceRegistration
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            // Configure Automapper && Mediator && Fluent Validation
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(AmortissementMappingProfile));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            //    foreach (var item in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.IsSubclassOf(typeof(AbstractValidator<>)))).ToArray())
            //    {
            //        services.AddScoped(item.GetType());
            //    }


            //    services.AddScoped<ContratValidator>();

            //}
        }
    }
}


//builder.Services.ConfigurationApplicationService();