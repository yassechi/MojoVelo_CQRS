using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;
using Mojo.Persistence.Repositories;

namespace Mojo.API.Dependencies
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    )
                );
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAmortissementRepository, AmortissementRepository>();
            services.AddScoped<IContratRepository, ContratRepository>();
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            services.AddScoped<IInterventionRepository, InterventionRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVeloRepository, VeloRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            // Tests
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}