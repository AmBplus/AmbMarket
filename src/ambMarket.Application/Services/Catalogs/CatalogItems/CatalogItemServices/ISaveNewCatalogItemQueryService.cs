namespace ambMarket.Application.Services.Catalogs.CatalogItems.CatalogItemServices
{
    public interface ISaveNewCatalogItemQueryService
    {
        List<ListCatalogBrandDto> GetBrand();
        List<ListCatalogTypeDto> GetCatalogType();
    }
}
