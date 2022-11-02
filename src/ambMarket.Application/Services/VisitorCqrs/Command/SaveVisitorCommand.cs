using _02_Framework.Application.Interfaces.Repositories;
using ambMarket.Domain.VisitorsAggregate;
using Mapster;
using MediatR;

namespace ambMarket.Application.Services.VisitorCqrs.Command;

public class RequestSaveVisitorCommand : IRequest<bool>
{
    public string Ip { get; set; }
    public string CurrentLink { get; set; }
    public string RefererLink { get; set; }
    public string Method { get; set; }
    public string Protocol { get; set; }
    public string PhysicalPath { get; set; }
    public VisitorVersionDto OperationSystem { get; set; }
    public VisitorVersionDto Browser { get; set; }
    public DeviceDto Device { get; set; }
    public DateTime CreateDateTime { get; set; }
    public string VisitorId { get; set; }
}

public class AddVisitorCommandHandler : IRequestHandler<RequestSaveVisitorCommand, bool>
{
    private IMongoDbRepository<Visitor> MongoDbRepository { get; }


    public AddVisitorCommandHandler(IMongoDbRepository<Visitor> mongoDbRepository)
    {
        MongoDbRepository = mongoDbRepository;
    }
    public async Task<bool> Handle(RequestSaveVisitorCommand request, CancellationToken cancellationToken)
    {
        var visitor = request.Adapt<Visitor>();
        MongoDbRepository.Add(visitor);
        return true;
    }
}
public class VisitorVersionDto{
    public string Family { get; set; }
    public string Version { get; set; }
}
public class DeviceDto
{
    public string Brand { get; set; }
    public string Family { get; set; }
    public string Model { get; set; }
    public bool IsSpider { get; set; }
}

