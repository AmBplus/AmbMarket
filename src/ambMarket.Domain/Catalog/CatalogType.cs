using _01_Framework.Domain.BaseEntities;
using Microsoft.Extensions.Logging;

namespace ambMarket.Domain.Catalog;


public class CatalogType :BaseEntityWithId<int>
{
    public int? ParentId { get; set; }
    public string Type { get; set; }
    public CatalogType Parent { get; set; }
    public List<CatalogType>? Childs { get; set; }
}