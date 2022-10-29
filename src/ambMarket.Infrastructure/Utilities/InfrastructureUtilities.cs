using ambMarket.Infrastructure.ConfigurationServices.Common;
using ambMarket.Infrastructure.ConfigurationServices.Cookie;
using ambMarket.Infrastructure.ConfigurationServices.DbConfig;
using ambMarket.Infrastructure.ConfigurationServices.Filter;
using ambMarket.Infrastructure.ConfigurationServices.Identity;
using ambMarket.Infrastructure.ConfigurationServices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.Utilities;

public static class InfrastructureUtilities
{
    public static string VisitorNameCookie
    {
        get => "AmbMartket-VisitorId";
    }
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
        services.DbServices(configuration);
        services.UserIdentityConfig(connectionString);
        services.BootstrapCookieConfig(configuration);
        services.BootstrapCommonServices(configuration);
        services.BootstrapRepositories(configuration);
        services.BootstrapFilterServices(configuration);
    }
    /// <summary>
    /// Get VisitorIdCookie , If Not Exists , Create then Return 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetVisitorId(this HttpContext context)
    {
        var visitorIdAsString = context.Request.Cookies[VisitorNameCookie];
        if (!string.IsNullOrWhiteSpace(visitorIdAsString))
        {
            return visitorIdAsString;
        }
        return SetVisitorIdCookie(context);
    }

    private static string SetVisitorIdCookie(HttpContext context )
    {
        var newVisitorId = Guid.NewGuid().ToString();
        context.Response.Cookies.Append(VisitorNameCookie, newVisitorId, new CookieOptions()
        {
            Path = "/",
            Expires = Shared.Utility.Now.AddDays(1),
            IsEssential = true,
        });
        return newVisitorId;
    }
}