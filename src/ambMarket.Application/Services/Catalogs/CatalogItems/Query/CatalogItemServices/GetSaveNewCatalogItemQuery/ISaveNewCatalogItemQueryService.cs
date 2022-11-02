namespace ambMarket.Application.Services.Catalogs.CatalogItems.Query.CatalogItemServices.GetSaveNewCatalogItemQuery
{
    public interface ISaveNewCatalogItemQueryService
    {
        List<ListCatalogBrandDto> GetBrand();
        List<ListCatalogTypeDto> GetCatalogType();
    }
}
