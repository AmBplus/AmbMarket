using ambMarket.Application.Services.VisitorCqrs.Command;
using ambMarket.Infrastructure.Utilities.Map.AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Common;

public static class CommonServices
{
    public static void BootstrapCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(RequestSaveVisitorCommand));
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddSignalR();
        services.Configure<Infrastructure.Settings.ApplicationSettings>
                (configuration.GetSection(key: nameof(Infrastructure.Settings.ApplicationSettings)))
            // AddSingleton()-> using Microsoft.Extensions.DependencyInjection;
            .AddSingleton
            (implementationFactory: serviceType =>
            {
                var result =
                    // GetRequiredService()-> using Microsoft.Extensions.DependencyInjection;
                    serviceType.GetRequiredService
                    <Microsoft.Extensions.Options.IOptions
                        <Infrastructure.Settings.ApplicationSettings>>().Value;

                return result;
            });
    }
}