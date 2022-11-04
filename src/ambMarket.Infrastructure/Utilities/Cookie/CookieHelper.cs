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
    public string BuyerIdCookieName { get => "amb-BuyerIdCookie"; }
    public string GetOrSetBuyerIdFromCookie(HttpContext context)
    {
        string buyerId;
        if (context.Request.Cookies.ContainsKey(BuyerIdCookieName))
        {
            context.Request.Cookies.TryGetValue(BuyerIdCookieName, out buyerId);
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
        context.Response.Cookies.Append(BuyerIdCookieName,buyerId,cookieOptions);
        return buyerId;
    }

    public string? GetBuyerIdFromCookie(HttpContext context)
    {
        string buyerIdFromCookie = string.Empty;
        if (context.Request.Cookies.ContainsKey(BuyerIdCookieName))
        {
            context.Request.Cookies.TryGetValue(BuyerIdCookieName, out buyerIdFromCookie);
        }
        return buyerIdFromCookie;
    }

    public void RemoveBuyerIdCookie(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey(BuyerIdCookieName))
        {
            context.Response.Cookies.Delete(BuyerIdCookieName);
        }
    }
}