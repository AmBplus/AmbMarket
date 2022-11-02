using ambMarket.Domain.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ambMarket.Persistence.EntityConfiguration;

public class CatalogTypesConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.HasQueryFilter(x => !x.Removed);
        // throw new NotImplementedException();
    }
}