using System.Reflection;
using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.BasketAggregate;
using ambMarket.Domain.CatalogAggregate;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Persistence.Contexts;

public class MarketDbContext : DbContext , IMarketDbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options ):base(options)
    {
        
    }

    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}