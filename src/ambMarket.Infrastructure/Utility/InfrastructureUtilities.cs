using ambMarket.Infrastructure.CookieConfig;
using ambMarket.Infrastructure.DbConfig;
using ambMarket.Infrastructure.IdentityConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.Utility;

public static class InfrastructureUtilities
{
    /// <summary>
    /// Get Sql Connection String
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string GetConnectionSql(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("SqlServerDb");
    }
    /// <summary>
    /// Get Main Connection String 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string GetMainDbConString(this IConfiguration configuration)
    {
        return GetConnectionSql(configuration);
    }
    /// <summary>
    /// WireUp All Services In Infrastructure Layer
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void WireUpInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = GetMainDbConString(configuration);
        services.DbConfig(connectionString);
        services.UserIdentityConfig(connectionString);
        services.BootstrapCookieConfig(configuration);
    }
}