using ambMarket.Application.Interfaces.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.Cmd.RemoveCatalogItem;

public class RequestRemoveCatalogItemService : IRequest<ResponseRemoveCatalogItemService>
{
    public long Id { get; set; }
}
public class ResponseRemoveCatalogItemService
{
    public ResultDto Result { get; set; }
}
public class RemoveCatalogItemServiceHandler : IRequestHandler<RequestRemoveCatalogItemService,ResponseRemoveCatalogItemService>
{
    public RemoveCatalogItemServiceHandler(IMarketDbContext context)
    {
        Context = context;
    }

    private IMarketDbContext Context { get; set; }
    public async Task<ResponseRemoveCatalogItemService> Handle(RequestRemoveCatalogItemService request, CancellationToken cancellationToken)
    {
        var response = new ResponseRemoveCatalogItemService();
        var catalogItem = await Context.CatalogItems.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (catalogItem == null)
        {
            response.Result = ResultDto.BuildFailedResult("پیدا نشد");
            return response;
        }
        // Catch Errir
        catalogItem.SetRemove();
        Context.CatalogItems.Update(catalogItem);
        await Context.SaveChangesAsync();

        response.Result = ResultDto.BuildSuccessResult();
        return response;
    }
}