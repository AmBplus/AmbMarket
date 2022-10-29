namespace ambMarket.Application.Services.Catalogs;

public class CatalogTypeDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int? ParentId { get; set; }
}