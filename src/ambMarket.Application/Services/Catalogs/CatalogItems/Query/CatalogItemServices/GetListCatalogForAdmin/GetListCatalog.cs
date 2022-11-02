using ambMarket.Application.Dto;
using ambMarket.Application.Interfaces.Databases;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.Query.CatalogItemServices.GetListCatalogForAdmin;

public class RequestGetListCatalogItemForAdmin : IRequest<ResponseGetListCatalogItemForAdmin>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = Constants.Page.PageSize;
    

}

public class ResponseGetListCatalogItemForAdmin
{
    public PaginateItemDto<List<GetListCatalogItemForAdminDto>> PaginateItemDto { get; set; }
}

public class GetListCatalogItemForAdminHandler : IRequestHandler<RequestGetListCatalogItemForAdmin,ResponseGetListCatalogItemForAdmin>
{
    public GetListCatalogItemForAdminHandler(IMarketDbContext context)
    {
        Context = context;
    }

    private IMarketDbContext Context { get; }
    public async Task<ResponseGetListCatalogItemForAdmin> Handle(RequestGetListCatalogItemForAdmin request, CancellationToken cancellationToken)
    {
        int pageCount;
       
        var dto = await Context.CatalogItems
            .ProjectToType<GetListCatalogItemForAdminDto>()
            .ToPaged(request.PageNumber, request.PageSize, out pageCount).ToListAsync();
        return new ResponseGetListCatalogItemForAdmin()
        {
            PaginateItemDto = new PaginateItemDto<List<GetListCatalogItemForAdminDto>>()
            {
                Data = dto,
                PageSize = request.PageSize,
                PageCount = pageCount,
                PageNumber = request.PageNumber
            }
        };
    }
}

public class GetListCatalogItemForAdminDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool Removed { get;  set; }
}