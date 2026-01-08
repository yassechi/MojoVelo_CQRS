
namespace Mojo.Application.Shared
{
    public static class ApplicationServiceRegistration
    {
        public static void ConfigurationApplicationService(this IServiceCollection services)
        {
            // Configure Automapper && Mediator && Fluent Validation
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(AmortissementMappingProfile));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        }
    }
}
