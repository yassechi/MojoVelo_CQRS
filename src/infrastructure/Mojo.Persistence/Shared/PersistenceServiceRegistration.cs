using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojo.Application.Model;
using Mojo.Domain.Entities;
using Mojo.Persistence.Repositories;

namespace Mojo.Persistence.Shared
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<MDbContext>()
            .AddDefaultTokenProviders();

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

            return services;
        }
    }
}