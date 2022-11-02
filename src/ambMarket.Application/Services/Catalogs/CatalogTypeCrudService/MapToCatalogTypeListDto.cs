using ambMarket.Domain.CatalogAggregate;

namespace ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;

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