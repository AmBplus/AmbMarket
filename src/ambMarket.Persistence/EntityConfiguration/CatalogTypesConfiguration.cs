using ambMarket.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ambMarket.Persistence.EntityConfiguration;

public class CatalogTypesConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
       // throw new NotImplementedException();
    }
}