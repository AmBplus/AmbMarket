using System.Security.Cryptography.X509Certificates;
using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.BasketAggregate;
using ambMarket.Domain.CatalogAggregate;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace ambMarket.Application.Services.Baskets;

public interface IBasketService
{
    Task<ResultDto<BasketDto>> GetOrCreateBasketForUser(string buyerId);
    Task<ResultDto<BasketDto?>> GetBasketForUser(string buyerId);
    Task<ResultDto<long>> GetOrCreateBasketIdForUser(string buyerId);
    Task<ResultDto> AddItemToBasket(long basketId, long catalogItemId, int quantity = 1);
    Task<ResultDto> RemoveItemFromBasket( long ItemId);
    Task<ResultDto> SetQuantities(long itemId, int quantity);

    Task<ResultDto> TransferUnknownUserBasketToUser(string unknownBuyer, string userId);

}
public class BasketService : IBasketService
{
    public BasketService(IMarketDbContext context)
    {
        Context = context;
    }

    private IMarketDbContext Context { get; }
    public async Task<ResultDto<BasketDto>> GetOrCreateBasketForUser(string buyerId)
    {
        var isBasketExists = await Context.Baskets.AnyAsync(x => x.BuyerId == buyerId);
        if (isBasketExists)
        {
            var basketQuery = Context.Baskets
                .Where(x => x.BuyerId == buyerId)
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(x=>x.CatalogItemImages);
               var mapBasketQueryDto = basketQueryMapToBasketDto(basketQuery); 
             var basketDto =  await mapBasketQueryDto.SingleOrDefaultAsync();
             if (basketDto == null)
             {
                return ResultDto<BasketDto>.BuildFailedResult();
             }
            return ResultDto<BasketDto>.BuildSuccessResult(basketDto);
        }
        var basketResult = await CreateBasketForUser(buyerId);
            if (basketResult.IsSuccess)
            {
                return ResultDto<BasketDto>.BuildSuccessResult(basketResult.Data);
            }
            return  ResultDto<BasketDto>.BuildFailedResult();
            
    }

    public async Task<ResultDto<BasketDto?>> GetBasketForUser(string buyerId)
    {
        var isExistsBasket = await Context.Baskets.AnyAsync(x => x.BuyerId == buyerId);
        if (isExistsBasket)
        {
            return await GetOrCreateBasketForUser(buyerId);
        }
        return ResultDto<BasketDto>.BuildFailedResult();
    }

    private IQueryable<BasketDto> basketQueryMapToBasketDto(IQueryable<Basket> query)
    {
        return query.Select(b => new BasketDto()
        {
            BuyerId = b.BuyerId,
            Id = b.Id,
            Items = b.Items.Select(i => new BasketItemDto()
            {
                Id = i.Id,
                BasketId = b.Id,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                CatalogItemId = i.CatalogItemId,
                CatalogName = i.CatalogItem.Name,
                ImageUrl = i.CatalogItem.CatalogItemImages.FirstOrDefault().Src ?? ""
            }).ToList()
        });
    }
    
    public async Task<ResultDto<long>> GetOrCreateBasketIdForUser(string buyerId)
    {
        var isBasketExists = await Context.Baskets.AnyAsync(x => x.BuyerId == buyerId);
        if (isBasketExists)
        {
           var id = await Context.Baskets.Where(x => x.BuyerId == buyerId).Select(x=>x.Id).FirstOrDefaultAsync();
           return ResultDto<long>.BuildSuccessResult(id);
        }

        var result = await CreateBasketForUser(buyerId);
        return ResultDto<long>.BuildSuccessResult(result.Data.Id);
    }
    private async Task<ResultDto<BasketDto>> CreateBasketForUser(string BuyerId)
    {
        var basket = new Basket(BuyerId);
        Context.Baskets.Add(basket);
        await Context.SaveChangesAsync();
        return ResultDto<BasketDto>.BuildSuccessResult
        (new BasketDto()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
        });
    }
  
    public async Task<ResultDto> AddItemToBasket(long basketId, long catalogItemId, int quantity = 1)
    {
        var basket = Context.Baskets.Include(x=>x.Items).FirstOrDefault(e => e.Id == basketId);
        if(basket == null) return ResultDto.BuildFailedResult("سبد پیدا نشد");
        var unitPrice = await Context.CatalogItems.Where(x=>x.Id == catalogItemId).Select(x=>x.Price)?.FirstOrDefaultAsync();
        if(unitPrice == 0) return ResultDto.BuildFailedResult("مبلغ کالا نامعتبر");
        if (Context.Entry<Basket>(basket).State == EntityState.Detached)
            Context.Baskets.Attach(basket);
        basket.AddBasketItem(unitPrice , quantity, catalogItemId);
        if (Context.Entry(basket).State == EntityState.Unchanged)
        {
            Context.Entry(basket).State = EntityState.Modified;
        }
        await Context.SaveChangesAsync();
        return ResultDto.BuildSuccessResult();
    }

    public async Task<ResultDto> RemoveItemFromBasket( long itemId)
    {
        var basketItem = Context.BasketItems.FirstOrDefault(e => e.Id == itemId);
        if (basketItem == null) return ResultDto.BuildFailedResult("پیدا نشد");
        if (Context.Entry<BasketItem>(basketItem).State == EntityState.Detached)
            Context.BasketItems.Attach(basketItem);
        basketItem.SetRemove();
        await Context.SaveChangesAsync();
        return ResultDto.BuildSuccessResult();
    }
    public async Task<ResultDto> SetQuantities(long itemId, int quantity)
    {
        var basketItem = Context.BasketItems.Include(x => x.CatalogItem).FirstOrDefault(e => e.Id == itemId);
        if (basketItem == null) return ResultDto.BuildFailedResult("پیدا نشد");
        if (Context.Entry<BasketItem>(basketItem).State == EntityState.Detached)
            Context.BasketItems.Attach(basketItem);
        basketItem.SetQuantity(quantity);
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (ArgumentOutOfRangeException e)
        {
            return ResultDto.BuildFailedResult("مقدار درخواستی نامعتبر است");
        }
        catch (Exception e)
        {
            return ResultDto.BuildFailedResult("مشکلی پیش آمده است");
        }
        return ResultDto.BuildSuccessResult();
    }

    public async Task<ResultDto> TransferUnknownUserBasketToUser(string unknownBuyer, string userId)
    {
        var isExistsAnyBasketForUnknownBuyer = await Context.Baskets.AnyAsync(x => x.BuyerId == unknownBuyer);
        if(!isExistsAnyBasketForUnknownBuyer) return ResultDto.BuildSuccessResult(); 
        Basket newBasket = new Basket(userId);
        Context.Baskets.Add(newBasket);
        var oldBasket = await Context.Baskets.Include(x=>x.Items).Where(x => x.BuyerId == unknownBuyer).FirstOrDefaultAsync();
       foreach (var item in oldBasket.Items)
       {
           if (Context.Entry(item).State == EntityState.Detached)
           {
               Context.BasketItems.Attach(item);
           }
           item.ChangeBasketItemId(newBasket.Id);
           if (Context.Entry(item).State == EntityState.Unchanged)
           {
               Context.Entry(item).State = EntityState.Modified;
           }
       }
       oldBasket.SetRemove();
       await Context.SaveChangesAsync();
       return ResultDto.BuildSuccessResult();
    }
}