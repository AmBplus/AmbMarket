using System.Security.Claims;
using ambMarket.Infrastructure.Utilities.Cookie;
using Shared.AspCore.Utility;

namespace Web.Endpoint.Infrastructure.WebUtility;

public class WebEndpointUtility
{
    private CookieHelper CookieHelper { get; set; }
    public WebEndpointUtility(CookieHelper cookieHelper)
    {
        CookieHelper = cookieHelper;
    }
    public string GetBuyerId(ClaimsPrincipal User,HttpContext context)
    {
        if (User.Identity.IsAuthenticated)
        {
            return ClaimUtility.GetUserId(User);
        }
        return CookieHelper.GetOrSetBuyerIdFromCookie(context);
    }
}