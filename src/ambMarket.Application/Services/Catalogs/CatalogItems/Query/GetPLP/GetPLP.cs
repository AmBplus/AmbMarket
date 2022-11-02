using ambMarket.Application.Dto;
using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.CatalogAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.Query.GetPLP
{
    public class RequestGetPLP : IRequest<ResponseGetPlp>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class ResponseGetPlp
    {
        public PaginateItemDto<List<GetPlpDto>> Result { get; set; }
    }
    public class GetPLPHandler : IRequestHandler<RequestGetPLP, ResponseGetPlp>
    {
        public GetPLPHandler(IMarketDbContext context)
        {
            Context = context;
        }

        private IMarketDbContext Context { get; set; }
        public async Task<ResponseGetPlp> Handle(RequestGetPLP request, CancellationToken cancellationToken)
        {
            int pageCount;
            var data = await Context.CatalogItems
                .Where(x=>!x.Removed)
                .Include(x => x.CatalogItemImages)
                .OrderByDescending(x => x.Id)
                .MapToGetPLPDto().ToPaged(request.PageNumber, request.PageSize, out pageCount)
                .ToListAsync();
            return new ResponseGetPlp()
            {
                Result = new PaginateItemDto<List<GetPlpDto>>()
                {
                    Data = data,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    PageCount = pageCount
                }
            };
        }


    }

    public static class MapperPlpDto
    {
        public static IQueryable<GetPlpDto> MapToGetPLPDto(this IQueryable<CatalogItem> query)
        {
            return query.Select(x => new GetPlpDto()
            {
                Id = x.Id,
                Image = x.CatalogItemImages.FirstOrDefault().Src ?? "",
                Name = x.Name,
                Price = x.Price,
                Rate = 4
            });
        }
    }
    public class GetPlpDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public byte Rate { get; set; }
        public string Image { get; set; }
    }
}
