namespace ambMarket.Application.Services.Baskets;

/// <summary>
/// سبد خرید
/// </summary>
public class BasketDto
{
    public long Id { get; set; }
    public string BuyerId { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
}