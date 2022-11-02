using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.CatalogAggregate;


public class CatalogType :BaseEntityWithId<int>
{
    public int? ParentId { get; set; }
    public string Type { get; set; }
    public CatalogType Parent { get; set; }
    public List<CatalogType>? Childs { get; set; }
}