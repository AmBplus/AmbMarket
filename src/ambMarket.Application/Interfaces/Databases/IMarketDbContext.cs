using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.BasketAggregate;
using ambMarket.Domain.CatalogAggregate;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Application.Interfaces.Databases;

public interface IMarketDbContext : IBaseDbContext
{
    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Basket> Baskets { get; set; }
}
