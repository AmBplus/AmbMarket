using ambMarket.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Shared;

namespace ambMarket.Infrastructure.Utilities.Cookie;

public class CookieHelper
{
    private ApplicationSettings ApplicationSettings { get; }
    public CookieHelper(ApplicationSettings applicationSettings)
    {
        this.ApplicationSettings = applicationSettings;
    }
    public string BuyerIdCookie { get => "amb-BuyerIdCookie"; }
    public string GetOrSetBuyerIdFromCookie(HttpContext context)
    {
        string buyerId;
        if (context.Request.Cookies.ContainsKey(BuyerIdCookie))
        {
            context.Request.Cookies.TryGetValue(BuyerIdCookie, out buyerId);
            if (!string.IsNullOrWhiteSpace(buyerId))
            {
                return buyerId;
            }
        }
        CookieOptions cookieOptions = new CookieOptions()
        {
            Path = "/",
            IsEssential = true,
            Secure = true,
            Expires = Utility.Now.AddSeconds(ApplicationSettings.ExpireBuyerIdCookieTimeBySeconds)
        };
        buyerId = Guid.NewGuid().ToString();
        context.Response.Cookies.Append(BuyerIdCookie,buyerId,cookieOptions);
        return buyerId;
    }
    
}