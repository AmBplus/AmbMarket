using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Application.Interfaces.Databases;
using ambMarket.Persistence.Contexts;
using ambMarket.Persistence.Contexts.MongoDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ambMarket.Infrastructure.ConfigurationServices.DbConfig;

public static class DatabaseConfig
{
    public static void DbServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IMarketDbContext,MarketDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("SqlServerDb")));
       // Add MongoDb ConnectionString
       services.AddSingleton(new MongoConnectionSettings(configuration.GetConnectionString("MongoDbConnectionName")));
       BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
       BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
       services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
    }
}