using ambMarket.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.DbConfig;

public static class DatabaseConfig
{
    public static void DbConfig(this IServiceCollection services, string connection)
    {
        services.AddDbContext<MarketDbContext>(op => op.UseSqlServer(connection));
    }
}