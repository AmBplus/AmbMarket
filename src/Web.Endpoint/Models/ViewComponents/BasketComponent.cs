using ambMarket.Application.Services.Baskets;
using ambMarket.Domain.userAggregate;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Shared.AspCore.Utility;
using System.Security.Claims;
using ambMarket.Infrastructure.Utilities.Cookie;
using Shared.Security;

namespace Web.Endpoint.Models.ViewComponents;

[ViewComponent]
public class BasketComponent : ViewComponent
{
    private CookieHelper CookieHelper { get; }
    private readonly IBasketService basketService;
    public BasketComponent(IBasketService basketService, CookieHelper cookieHelper)
    {
        this.basketService = basketService;
        CookieHelper = cookieHelper;
    }
    private ClaimsPrincipal userClaimsPrincipal => ViewContext?.HttpContext?.User;
    public async Task<IViewComponentResult> InvokeAsync()
    {
        BasketDto basket = null;
        if (User.Identity.IsAuthenticated)
        {
            var result =await basketService.GetBasketForUser(ClaimUtility.GetUserId(userClaimsPrincipal));
            basket = result.Data;
        }
        else
        {
            if (Request.Cookies.ContainsKey(CookieHelper.BuyerIdCookieName))
            {
                var buyerId = Request.Cookies[CookieHelper.BuyerIdCookieName];
                var result = await basketService.GetBasketForUser(buyerId);
                basket = result.Data;
            }

        }
        return View(viewName: "BasketComponent", model: basket);
    }
    
}