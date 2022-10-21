using ambMarket.Domain.userAggregate;
using ambMarket.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.IdentityConfig;

public static class UserConfig
{
    public static void UserIdentityConfig(this IServiceCollection services, string connection)
    {
        services.AddDbContext<IdentityDbContext>(op => op.UseSqlServer(connection));
        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<CustomIdentityError>();

    }
}