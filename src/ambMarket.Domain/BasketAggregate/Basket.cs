using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.BasketAggregate;

public class Basket : BaseEntityWithId<long>
{
    public List<BasketItem> Items { get; }

    public Basket(string buyerId)
    {
        BuyerId = buyerId;
    }
    public string BuyerId { get; set; }

    public void AddBasketItem(int unitPrice, int quantity, long catalogItemId)
    {
        if(!Items.Any(x=>x.CatalogItemId == catalogItemId))
        {
            Items.Add(new BasketItem(unitPrice, quantity, Id, catalogItemId));
            return;
        }
        var basketItem = Items.SingleOrDefault(x => x.CatalogItemId == catalogItemId);
        basketItem.IncreaseQuantity(quantity);
    }
    public void RemoveBasketItem(long itemId)
    {
        var basketItem = Items.Where(x => x.Id == itemId).SingleOrDefault();
        if(basketItem != null)
        Items.Remove(basketItem);
    }
}