using ambMarket.Application.Services.Baskets;
using ambMarket.Domain.BasketAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Endpoint.Infrastructure.WebUtility;

namespace Web.Endpoint.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class BasketsController : ControllerBase
{
    public BasketsController(IBasketService basketService, WebEndpointUtility webEndpointUtility)
    {
        BasketService = basketService;
        WebEndpointUtility = webEndpointUtility;
    }
    private IBasketService BasketService { get; }
    private WebEndpointUtility WebEndpointUtility { get; }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RemoveItemFromBasket(long itemId)
    {
       var result = await BasketService.RemoveItemFromBasket(itemId);
       if (result.IsSuccess)
       {
           return Ok("حذف شد");
       }
       return BadRequest(new {message= result.Message });
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SetQuantity( long basketItemId , int quantity)
    {
        var result = await BasketService.SetQuantities(basketItemId, quantity);
        if (result.IsSuccess)
        {
            return Ok(new {message="ثبت شد"});
        }
        return BadRequest(new {message=result.Message});
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> AddToCart(long itemId)
    {
        var buyerId = WebEndpointUtility.GetBuyerId(User, HttpContext);
        var basket = await BasketService.GetOrCreateBasketIdForUser(buyerId);
        await BasketService.AddItemToBasket(basket.Data, itemId);
        return Redirect("/Baskets/Index");
    }
}