using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.CatalogAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.Query.GetPdp;

public class RequestGetPdp : IRequest<ResultDto<GetPdpDto>>
{
    public long Id { get; set; }
}

public class GetPdpHandler  : IRequestHandler<RequestGetPdp, ResultDto<GetPdpDto>>
{
    public GetPdpHandler(IMarketDbContext context)
    {
        Context = context;
    }

    private IMarketDbContext Context { get; set; }
    public async Task<ResultDto<GetPdpDto>> Handle(RequestGetPdp request, CancellationToken cancellationToken)
    {
        //var query = Context.CatalogItems
        //    .Include(x => x.CatalogBrand)
        //    .Include(x => x.CatalogType)
        //    .Include(x => x.CatalogItemImages)
        //    .Include(x => x.CatalogItemFeatures)
        //    .Where(x => x.Id == request.Id);
        var catalogItem = await Context.CatalogItems
            .Include(x => x.CatalogBrand)
            .Include(x => x.CatalogType)
            .Include(x => x.CatalogItemImages)
            .Include(x => x.CatalogItemFeatures)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync();
        //var Pdp = await MapCatalogItemToPdp(query).FirstOrDefaultAsync();
        if(catalogItem == null)
        {
            return ResultDto<GetPdpDto>.BuildFailedResult("محصول پیدا نشد");
        }
        var Pdp = MapCatalogItemToPdpDto(catalogItem);
        
        Pdp.PdpSimilarProduct = Context.CatalogItems.Take(5)
            .Where(c => c.CatalogTypeId == catalogItem.CatalogTypeId)
            .Select(p => new GetPdpSimilarProductDto()
            {
                Price = p.Price,
                Id = p.Id,
                Image = p.CatalogItemImages.FirstOrDefault().Src,
                Name = p.Name
            }).ToList();
        return ResultDto<GetPdpDto>.BuildSuccessResult(Pdp);
    }

    public IQueryable<GetPdpDto> MapCatalogItemToPdp(IQueryable<CatalogItem> query)
    {
        return query.Select(x => new GetPdpDto
        {
            Brand = x.CatalogBrand.Name,
            Description = x.Description,
            Feature = x.CatalogItemFeatures.Select(p => new GetPdpFeatureDto()
            {
                Value = p.Value,
                Group = p.Group,
                Key = p.Key
            }).GroupBy(g => g.Group),
            Id = x.Id,
            Images = x.CatalogItemImages.Select(i => i.Src).ToList(),
            Name = x.Name,
            //PdpSimilarProduct = Context.CatalogItems.Take(5)
            //    .Where(c => c.CatalogBrandId == x.CatalogTypeId)
            //    .Select(p => new GetPdpSimilarProductDto()
            //    {
            //        Price = p.Price,
            //        Id = p.Id,
            //        Image = p.CatalogItemImages.FirstOrDefault().Src,
            //        Name = p.Name
            //    }).ToList(),
            Price = x.Price,
            Type = x.CatalogType.Type
        });
    }
    public GetPdpDto MapCatalogItemToPdpDto(CatalogItem catalogItem)
    {
        return new GetPdpDto 
        {
            Brand = catalogItem.CatalogBrand.Name,
            Description = catalogItem.Description,
            Feature = catalogItem.CatalogItemFeatures.Select(p => new GetPdpFeatureDto()
            {
                Value = p.Value,
                Group = p.Group,
                Key = p.Key
            }).GroupBy(g => g.Group),
            Id = catalogItem.Id,
            Images = catalogItem.CatalogItemImages.Select(i => i.Src).ToList(),
            Name = catalogItem.Name,
            Price = catalogItem.Price,
            Type = catalogItem.CatalogType.Type
        };
    }
}

public class GetPdpDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }
    public List<string> Images { get; set; }
    public string Description { get; set; }
    public IEnumerable<IGrouping<string,GetPdpFeatureDto>> Feature { get; set; }
    public List<GetPdpSimilarProductDto> PdpSimilarProduct { get; set; }
}

public class GetPdpFeatureDto
{
    public string Group{ get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}

public class GetPdpSimilarProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
}
