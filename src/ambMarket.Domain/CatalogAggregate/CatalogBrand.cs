using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.CatalogAggregate;

public class CatalogBrand : BaseEntityWithId<int>
{
    public string Name { get; set; }
    
}