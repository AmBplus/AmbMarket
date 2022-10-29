using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ambMarket.Persistence.Contexts;

public class MarketDbContext : DbContext , IMarketDbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options ):base(options)
    {
        
    }

    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
}