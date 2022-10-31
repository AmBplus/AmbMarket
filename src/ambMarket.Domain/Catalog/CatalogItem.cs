using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.Catalog;

public class CatalogItem : BaseEntityWithId<long>
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; }
    public int CatalogBrandId { get; set; }
    public CatalogBrand CatalogBrand { get; set; }
    public int AvailabeStock { get; set; }
    public int RentStock { get; set; }
    public int MaxStockThreshold { get; set; }
    public ICollection<CatalogItemFeature> CatalogItemFeatures { get; set; }
    public ICollection<CatalogItemImage> CatalogItemImages { get; set; }
}
 
public class CatalogItemFeature 
{
    public long Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string Group { get; set; }
    public CatalogItem CatalogItem { get; set; }
    public long CatalogItemId { get; set; }
}

public class CatalogItemImage
{
    public int Id { get; set; }
    public string Src { get; set; }
    public CatalogItem CatalogItem { get; set; }
    public long CatalogItemId { get; set; }
}