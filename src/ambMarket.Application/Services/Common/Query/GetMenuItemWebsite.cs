using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.Catalog;
using MediatR;

namespace ambMarket.Application.Services.Common.Query;

public class RequestGetMenuItemWebsite:IRequest<ResponseGetMenuItemWebsite>
{
    
}

public class ResponseGetMenuItemWebsite
{
    public List<MenuItemDto> MenuItemDtos { get; set; }
}

public class MenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
}

public class GetMenuItemWebsiteHandler : IRequestHandler<RequestGetMenuItemWebsite,ResponseGetMenuItemWebsite>
{
    public GetMenuItemWebsiteHandler(IMarketDbContext context)
    {
        Context = context;
    }

    private IMarketDbContext Context { get; }
    public async Task<ResponseGetMenuItemWebsite> Handle(RequestGetMenuItemWebsite request, CancellationToken cancellationToken)
    {
        var query = Context.CatalogTypes;
        var querySelectedList = MapToMenuItemDto(query);
        var result = querySelectedList.ToList();
        return new ResponseGetMenuItemWebsite()
        {
            MenuItemDtos = result
        };
    }
    public IQueryable<MenuItemDto> MapToMenuItemDto(IQueryable<CatalogType> query)
    {
        return query.Select(x => new MenuItemDto()
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Name = x.Type
        });
    }
}

