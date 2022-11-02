using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.CatalogAggregate;
using MediatR;
using Shared.Dto;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.Cmd.AddNewCatalogItem;


public class NewCatalogItemHandler : IRequestHandler<RequestSaveNewCatalogItemCmd, ResultDto<long>>
{
    public AutoMapper.IMapper Mapper { get; }
    private readonly IMarketDbContext context;
    public NewCatalogItemHandler(IMarketDbContext context, AutoMapper.IMapper mapper)
    {
        Mapper = mapper;
        this.context = context;
    }


    public async Task<ResultDto<long>> Handle(RequestSaveNewCatalogItemCmd request, CancellationToken cancellationToken)
    {
        var catalogItem = Mapper.Map<CatalogItem>(request);
        context.CatalogItems.Add(catalogItem);
        try
        {
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return ResultDto<long>.BuildSuccessResult(catalogItem.Id);
    }
}


