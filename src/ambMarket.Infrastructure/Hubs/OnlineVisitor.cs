using ambMarket.Application.Services.VisitorOnlineRepository;
using ambMarket.Infrastructure.Utilities;
using Microsoft.AspNetCore.SignalR;

namespace ambMarket.Infrastructure.Hubs;

public class OnlineVisitor : Hub
{
    public OnlineVisitor(IVisitorOnlineService onlineVisitorService)
    {
        OnlineVisitorService = onlineVisitorService;
    }

    private IVisitorOnlineService OnlineVisitorService { get; }
    public override async Task OnConnectedAsync()
    {
        var visitorId = Context.GetHttpContext().GetVisitorId();
        await OnlineVisitorService.ConnectUserAsync(visitorId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var visitorId = Context.GetHttpContext().GetVisitorId();
        await OnlineVisitorService.DisconnectUserAsync(visitorId);
        await base.OnDisconnectedAsync(exception);
    }
}