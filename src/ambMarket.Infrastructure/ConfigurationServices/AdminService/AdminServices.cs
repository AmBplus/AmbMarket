using ambMarket.Infrastructure.ExternalApi.ImageServer;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.AdminService;

public static class AdminServices
{
    public static void BootstrapCustomAdminServices(this IServiceCollection services)
    {
        services.AddTransient<IImageUploadService, ImageUploadService>();
    }
}