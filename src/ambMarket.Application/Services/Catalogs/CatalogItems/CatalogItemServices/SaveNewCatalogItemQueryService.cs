using ambMarket.Application.Interfaces.Databases;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.CatalogItemServices;

public class SaveNewCatalogItemQueryService : ISaveNewCatalogItemQueryService
{

    private readonly IMarketDbContext context;
    private readonly IMapper mapper;

    public SaveNewCatalogItemQueryService(IMarketDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<ListCatalogBrandDto> GetBrand()
    {
        var brands = context.CatalogBrands
            .OrderBy(p => p.Name)
            .ToList();

        var data = mapper.Map<List<ListCatalogBrandDto>>(brands);
        return data;
    }

    public List<ListCatalogTypeDto> GetCatalogType()
    {
        var types = context.CatalogTypes
            .Include(p => p.Parent)
            .ThenInclude(p => p.Parent.Parent)
            .Include(p => p.Childs)
            .Where(p => p.ParentId != null)
            .Where(p => p.Childs.Count == 0)
            .Select(p => new { p.Id, p.Type, p.Parent, p.Childs})
            .ToList()
            .Select(p => new ListCatalogTypeDto
            {
                Id = p.Id,
                Type = $"{p?.Type ?? ""} - {p?.Parent?.Type ?? ""} - {p?.Parent?.Parent?.Type ?? ""}"
            }).ToList();
        return types;
    }
}