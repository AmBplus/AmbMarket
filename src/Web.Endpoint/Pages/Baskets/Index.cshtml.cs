using ambMarket.Application.Services.Baskets;
using ambMarket.Infrastructure.Utilities.Cookie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.AspCore.Utility;
using Web.Endpoint.Infrastructure.WebUtility;

namespace Web.Endpoint.Pages.Baskets
{
    public class IndexModel : PageModel
    {
        public IndexModel(IBasketService basketService, WebEndpointUtility webEndpointUtility)
        {
            BasketService = basketService;
            WebEndpointUtility = webEndpointUtility;
        }
        private WebEndpointUtility WebEndpointUtility { get;  }
        private IBasketService BasketService { get; }
        public BasketDto? BasketDto { get; set; }
        public async Task OnGet()
        {
            string buyerId = WebEndpointUtility.GetBuyerId(User, HttpContext);
            var result = await BasketService.GetBasketForUser(buyerId);
            if (result.IsSuccess)
            {
                BasketDto = result.Data ?? new BasketDto();
                return;
            }
            BasketDto = new BasketDto();
        }
   
    }
}
