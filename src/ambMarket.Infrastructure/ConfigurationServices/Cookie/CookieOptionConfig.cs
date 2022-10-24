using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Cookie;

public static class CookieOptionConfig
{
    public static void BootstrapCookieConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureApplicationCookie(op =>
        {
            op.ExpireTimeSpan = TimeSpan.FromDays(1);
            op.LoginPath = "/account/login";
            op.LogoutPath = "/account/logout";
            op.AccessDeniedPath = "/account/accessDenied";
            op.SlidingExpiration = true;
        });
    }
}