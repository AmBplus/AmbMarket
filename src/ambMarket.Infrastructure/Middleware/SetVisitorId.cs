using ambMarket.Infrastructure.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ambMarket.Infrastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SetVisitorId
    {
        private readonly RequestDelegate _next;

        public SetVisitorId(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.GetVisitorId();
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SetVisitorIdExtensions
    {
        public static IApplicationBuilder UseSetVisitorId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetVisitorId>();
        }
    }
}
