using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Persistence.Repositories;

namespace Mojo.Persistence.Shared
{
    public static class PersistenceServiceRegistration
    {
        public static void ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure MDbContext
            services.AddDbContext<MDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Configure Services 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAmortissementRepository, AmortissementRepository>();
            services.AddScoped<IContratRepository, ContratRepository>();
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            services.AddScoped<IInterventionRepository, InterventionRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVeloRepository, VeloRepository>();
        }
    }
}
