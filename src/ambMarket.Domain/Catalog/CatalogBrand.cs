using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.Catalog;

public class CatalogBrand : BaseEntityWithId<int>
{
    public string Name { get; set; }
    
}