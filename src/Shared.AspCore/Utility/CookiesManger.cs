using Microsoft.AspNetCore.Http;

namespace Shared.AspCore.Utility
{
    public static class CookiesManger
    {
        static CookiesManger()
        {
                
        }
        public static void AddCookie(HttpContext context, string token, string value , int expDay)
        {
            
            context.Response.Cookies.Append(token, value, GetCookieOptions(context,expDay));
        }

        public static bool CookieIsExist(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }

        public static string GetCookie(HttpContext context, string token)
        {
            string cookieValue;
            if (!context.Request.Cookies.TryGetValue(token, out cookieValue))
            {
                return null;
            }
            return cookieValue;
        }

        public static void RemoveCookie(HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token))
            {
                context.Response.Cookies.Delete(token);
            }
        }


        public static Guid GetDeviceIdFromCookie(HttpContext context)
        {
            string browserId = GetCookie(context, "DeviceId");
            Guid guidBowser;
            Guid.TryParse(browserId, out guidBowser);
            return guidBowser;
        }
        private static CookieOptions GetCookieOptions(HttpContext context,int expDay)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = Shared.Utility.Now.AddDays(expDay),
            };
        }
    }
}
