using ambMarket.Application.Services.VisitorOnlineRepository;
using ambMarket.Infrastructure.Utilities;
using Microsoft.AspNetCore.SignalR;

namespace ambMarket.Infrastructure.Hubs;

public class OnlineVisitor : Hub
{
    public OnlineVisitor(IVisitorOnlineRepositoryService onlineVisitorRepository)
    {
        OnlineVisitorRepository = onlineVisitorRepository;
    }

    private IVisitorOnlineRepositoryService OnlineVisitorRepository { get; }
    public override async Task OnConnectedAsync()
    {
        var visitorId = Context.GetHttpContext().GetVisitorId();
        await OnlineVisitorRepository.ConnectUserAsync(visitorId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var visitorId = Context.GetHttpContext().GetVisitorId();
        await OnlineVisitorRepository.DisconnectUserAsync(visitorId);
        await base.OnDisconnectedAsync(exception);
    }
}