using _02_Framework.Application.Interfaces.Repositories;
using ambMarket.Application.Services.VisitorOnlineRepository;
using ambMarket.Persistence.Repositories.GenericRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Repository;

public static class RepositoryConfiguration
{
    public static void BootstrapRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // Generic MongoDb Repository
        services.AddTransient(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
        services.AddScoped<IVisitorOnlineRepositoryService, VisitorOnlineRepositoryService>();
    }
}