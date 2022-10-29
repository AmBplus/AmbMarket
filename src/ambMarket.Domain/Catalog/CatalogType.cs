using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.Catalog;


public class CatalogType :BaseEntityWithId<int>
{
    public int? ParentId { get; set; }
    public CatalogType Parent { get; set; }
    public List<CatalogType>? Childs { get; set; }
}

public class CatalogBrand : BaseEntityWithId<int>
{
    public string Name { get; set; }
    
}

public class CatalogItem : BaseEntityWithId<int>
{
    
}