using ambMarket.Domain.Catalog;

namespace ambMarket.Application.Services.Catalogs.CatalogTypeService;

public static class MapToCatalogTypeListDto
{

    public static IQueryable<CatalogTypeListDto> MapCatalogTypeToCatalogTypeListDtos(
        this IQueryable<CatalogType> CatalogTypesQuery)
    {
        return CatalogTypesQuery.Select(x => new CatalogTypeListDto()
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Type = x.Type,
            SubTypeCount = x.Childs.Count
        });
    }
}