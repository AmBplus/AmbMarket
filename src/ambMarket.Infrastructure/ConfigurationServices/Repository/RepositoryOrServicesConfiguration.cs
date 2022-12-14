using _02_Framework.Application.Interfaces.Repositories;
using ambMarket.Application.Services.Baskets;
using ambMarket.Application.Services.Catalogs.CatalogBrandCrudService;
using ambMarket.Application.Services.Catalogs.CatalogItems.Query.CatalogItemServices.GetSaveNewCatalogItemQuery;
using ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;
using ambMarket.Application.Services.VisitorOnlineRepository;
using ambMarket.Persistence.Repositories.GenericRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Repository;

public static class RepositoryOrServicesConfiguration
{
    public static void BootstrapRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // Generic MongoDb Repository
        services.AddTransient(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
        services.AddScoped<IVisitorOnlineService, VisitorOnlineService>();
        services.AddScoped<ICatalogTypeService, CatalogTypeService>();
        services.AddScoped<ICatalogBrandService, CatalogBrandService>();
        services.AddScoped<ISaveNewCatalogItemQueryService, SaveNewCatalogItemQueryService>();
        services.AddScoped<IBasketService, BasketService>();
    }
}