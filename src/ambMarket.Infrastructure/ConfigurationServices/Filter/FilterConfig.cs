using ambMarket.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Filter;

public static class FilterConfig
{
    public static void BootstrapFilterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AddVisitorToPageAttribute>();
    }
}