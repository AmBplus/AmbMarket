using ambMarket.Application.Interfaces.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ambMarket.Persistence.Contexts;

public class MarketDbContext : DbContext , IMarketDbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options ):base(options)
    {
        
    }
}