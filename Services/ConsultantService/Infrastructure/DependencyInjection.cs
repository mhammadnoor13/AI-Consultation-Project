// Infrastructure/DependencyInjection.cs
using Application.Common.Persistence;
using Infrastructure.Persistence.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration config)
        {
            // 1) MongoClient interface
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(config.GetConnectionString("Mongo")));

            // 2) MongoDatabase interface
            services.AddScoped<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>()
                  .GetDatabase(config["Mongo:DatabaseName"]));

            // 3) Your persistence abstractions
            services.AddScoped<IConsultantRepository, MongoConsultantRepository>();
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();

            return services;
        }

    }
}
