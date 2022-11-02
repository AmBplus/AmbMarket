
using ambMarket.Application.Interfaces.Databases;
using MediatR;
using Shared.Dto;

namespace ambMarket.Application.Services.Baskets.Cmd;

public class RequestCreateBasket : IRequest<ResultDto<BasketDto>>
{
    public  string BuyerId { get; set; }
}

public class CreateBasketHandler : IRequestHandler<RequestCreateBasket,ResultDto<BasketDto>>
{
    private IMarketDbContext Context { get; }
    public async Task<ResultDto<BasketDto>> Handle(RequestCreateBasket request, CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
    }
}
public class BasketDto{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
}
public class BasketItemDto
{
    public int Id { get; set; }
    public int CatalogItemid { get; set; }
    public string CatalogName { get; set; }
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
}