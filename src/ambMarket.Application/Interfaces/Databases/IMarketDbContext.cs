using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Application.Interfaces.Databases;

public interface IMarketDbContext : IBaseDbContext
{
    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
}
