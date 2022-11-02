namespace ambMarket.Application.Services.Catalogs.CatalogItems.Cmd.UnRemoveCatalogItem;

using ambMarket.Application.Interfaces.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;



public class RequestUnRemoveCatalogItemService : IRequest<ResponseUnRemoveCatalogItemService>
{
    public long Id { get; set; }
}
public class ResponseUnRemoveCatalogItemService
{
    public ResultDto Result { get; set; }
}
public class UnRemoveCatalogItemServiceHandler : IRequestHandler<RequestUnRemoveCatalogItemService, ResponseUnRemoveCatalogItemService>
{
    public UnRemoveCatalogItemServiceHandler(IMarketDbContext context)
    {
        Context = context;
    }

    private IMarketDbContext Context { get; set; }
    public async Task<ResponseUnRemoveCatalogItemService> Handle(RequestUnRemoveCatalogItemService request, CancellationToken cancellationToken)
    {
        var response = new ResponseUnRemoveCatalogItemService();
        var catalogItem = await Context.CatalogItems.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (catalogItem == null)
        {
            response.Result = ResultDto.BuildFailedResult("پیدا نشد");
            return response;
        }
        // Catch Errir
        catalogItem.UnRemove();
        Context.CatalogItems.Update(catalogItem);
        await Context.SaveChangesAsync();

        response.Result = ResultDto.BuildSuccessResult();
        return response;
    }
}