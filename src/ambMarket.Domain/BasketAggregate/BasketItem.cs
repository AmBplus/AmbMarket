using _01_Framework.Domain.BaseEntities;
using ambMarket.Domain.CatalogAggregate;

namespace ambMarket.Domain.BasketAggregate;

public class BasketItem : BaseEntityWithId<long>
{
    #region Constructor
    public BasketItem(int unitPrice, int quantity, long basketId, long catalogItemId)
    {
        UnitPrice = unitPrice;
        Quantity = quantity;
        BasketId = basketId;
        CatalogItemId = catalogItemId;
    }
    #endregion /Constructor
 
    #region Properties

    /// <summary>
    /// جمع قیمت ها
    /// </summary>
    public int UnitPrice { get; private set; }

    /// <summary>
    /// تعداد محصول در سبد خرید
    /// </summary>
    public int Quantity { get; private set; }

    public long BasketId { get; private set; }
    public long CatalogItemId { get; private set; }
    public CatalogItem CatalogItem { get; private set; }

    #endregion /Properties

    #region Methods

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public bool DecreaseQuantity(int quantity)
    {
        if (Quantity - quantity < 0)
        {
            throw new ArgumentOutOfRangeException("قیمت نمیتواند کمتر از صفر باشد");
        }
        Quantity -= quantity;
        return true;
    }

    #endregion /Methods
}