#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0070

using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Contracts.Identity;
using Mojo.Application.Persistance.Emails;
using Mojo.Infrastructure.AI;
using Mojo.Infrastructure.Email;
using Mojo.Infrastructure.Services;
using Mojo.Persistence.DatabaseContext;
//using Microsoft.SemanticKernel.Connectors.InMemory;

namespace Mojo.API.Dependencies
{
    public static class InfrastructureServiceRegistration
    {
        public static void ConfigureInfrastructureService(
            this IServiceCollection services, IConfiguration configuration)
        {
            // Email
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IAuthService, AuthService>();

            // AI
            services.AddMemoryCache();
            services.AddAIServices(configuration);
        }

        private static IServiceCollection AddAIServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            var ollamaEndpoint = new Uri(configuration["Ollama:Endpoint"]!);
            var connectionString = configuration.GetConnectionString("DefaultConnection")!;

            // Kernel LLM principal (llama3.2:1b)
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddOllamaChatCompletion(
                modelId: "llama3.2:1b",
                endpoint: ollamaEndpoint
            );
            kernelBuilder.AddOllamaEmbeddingGenerator(
                modelId: "nomic-embed-text",
                endpoint: ollamaEndpoint
            );
            var kernel = kernelBuilder.Build();
            services.AddSingleton(kernel);

            // Kernel Juge (gemma3:4b)
            var jugeKernelBuilder = Kernel.CreateBuilder();
            jugeKernelBuilder.AddOllamaChatCompletion(
                modelId: "gemma3:4b",
                endpoint: ollamaEndpoint
            );
            var jugeKernel = jugeKernelBuilder.Build();

            services.AddInMemoryVectorStore();

            services.AddScoped<IRagService>(sp => new RagService(
                vectorStore: sp.GetRequiredService<VectorStore>(),
                embeddingService: kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>(),
                chatService: kernel.GetRequiredService<IChatCompletionService>(),
                jugeService: jugeKernel.GetRequiredService<IChatCompletionService>(),
                cache: sp.GetRequiredService<IMemoryCache>(),
                dbContext: sp.GetRequiredService<MDbContext>(),
                configuration: sp.GetRequiredService<IConfiguration>()
            ));

            return services;
        }
    }
}