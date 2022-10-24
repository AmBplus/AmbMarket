using ambMarket.Application.Services.VisitorCqrs.Command;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace ambMarket.Infrastructure.Utilities.Map;

public class MapUtilityInfrastructure
{
    public static RequestSaveVisitorCommand MapHttpContextToRequestSaveVisitorDto(HttpContext context)
    {
        var userAgent = context.Request.Headers["User-Agent"];
        var uaParser = Parser.GetDefault();
        ClientInfo clientInfo = uaParser.Parse(userAgent);
        var requestSaveVisitor = new RequestSaveVisitorCommand()
        {
            Ip = context.Request.HttpContext.Connection.RemoteIpAddress?.ToString()!,

            CurrentLink = context.Request.Path,
            Browser = new VisitorVersionDto()
            {
                Family = clientInfo.UA.Family,
                Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}",
            },
            Device = new DeviceDto()
            {
                Model = clientInfo.Device.Model,
                Family = clientInfo.Device.Family,
                Brand = clientInfo.Device.Brand,
                IsSpider = clientInfo.Device.IsSpider,
            },
            Method = context.Request.Method,
            OperationSystem = new VisitorVersionDto()
            {
                Family = clientInfo.OS.Family,
                Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
            },
            PhysicalPath = context.Request.Path,
            Protocol = context.Request.Protocol,
            RefererLink = context.Request.Headers["Referer"].ToString(),
            CreateDateTime = DateTime.Now,
            VisitorId = context.GetVisitorId()
        };
        return requestSaveVisitor;
    }


}