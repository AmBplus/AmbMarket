using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.VisitorsAggregate;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shared;

namespace ambMarket.Application.Services.VisitorOnlineRepository;

public interface IVisitorOnlineService
{
    Task ConnectUserAsync(string clientId);
    Task DisconnectUserAsync(string clientId);
    Task<int> GetVisitorOnlineCountAsync();
}

public class VisitorOnlineService : IVisitorOnlineService
{
    public VisitorOnlineService(IMongoDbContext<OnlineVisitor> visitorOnlineCollection)
    {
        VisitorOnlineCollection = visitorOnlineCollection.GetCollection();
    }
    private IMongoCollection<OnlineVisitor> VisitorOnlineCollection { get; }
    public async Task ConnectUserAsync(string clientId)
    {
        var ExistsVisitorOnline = await VisitorOnlineCollection.AsQueryable().AnyAsync(x => x.ClientId == clientId);
        if(ExistsVisitorOnline) return;
        await VisitorOnlineCollection.InsertOneAsync
            (new OnlineVisitor() { CreateDateTime = Utility.Now, ClientId = clientId });
    }

    public async Task DisconnectUserAsync(string clientId)
    {
      await  VisitorOnlineCollection.FindOneAndDeleteAsync(x => x.ClientId == clientId);
    }

    public async Task<int> GetVisitorOnlineCountAsync()
    {
        return await VisitorOnlineCollection.AsQueryable().CountAsync();
    }
}