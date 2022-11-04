namespace ambMarket.Application.Services.Baskets;

/// <summary>
/// آیتم سبد خرید
/// </summary>
public class BasketItemDto
{
    public long Id { get; set; }
    public long CatalogItemId { get; set; }
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
    public long BasketId { get; set; }
    public string CatalogName { get; set; }
    public string ImageUrl { get; set; }
}