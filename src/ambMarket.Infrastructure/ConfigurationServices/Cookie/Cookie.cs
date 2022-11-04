using ambMarket.Infrastructure.Utilities.Cookie;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Cookie;

public static class Cookie
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
        services.AddSingleton<CookieHelper>();
    }
}