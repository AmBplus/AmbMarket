using ambMarket.Domain.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ambMarket.Persistence.EntityConfiguration;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>

{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasQueryFilter(x => !x.Removed);
        builder.HasIndex(x => x.BuyerId);
    }
}