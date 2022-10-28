using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.Visitors;
using MediatR;
using MongoDB.Driver;

namespace ambMarket.Application.Services.VisitorCqrs.Query;

public class RequestGetLatestVisitorWebsiteInfo : IRequest<ResponseGetLatestVisitorWebsiteInfo>
{
    public int count { get; set; } = 10;
}

public class ResponseGetLatestVisitorWebsiteInfo
{
    public List<GetLatestVisitorWebsiteInfoDto> GetLatestVisitors { get; set; }
}

public class GetLatestVisitorWebsiteInfoDto
{
    public Guid Id { get; set; }
    public string CurrentLink { get; set; }
    public string RefererLink { get; set; }
    public string PhysicalPath { get; set; }
    public string OperationSystem { get; set; }
    public string Browser { get; set; }
    public string Device { get; set; }
    public string Ip { get; set; }
    public bool IsSpider { get; set; }
    public DateTime CreateDateTime { get; set; }
    public Guid VisitorId { get; set; }
}
public class GetLatestVisitorWebsiteInfoHandler : IRequestHandler<RequestGetLatestVisitorWebsiteInfo,ResponseGetLatestVisitorWebsiteInfo>
{
    private IMongoCollection<Visitor> VisitorCollection { get; }

    public GetLatestVisitorWebsiteInfoHandler(IMongoDbContext<Visitor> mongoDbVisitor)
    {
        VisitorCollection = mongoDbVisitor.GetCollection();
    }
    public async Task<ResponseGetLatestVisitorWebsiteInfo> Handle(RequestGetLatestVisitorWebsiteInfo request, CancellationToken cancellationToken)
    {
      var result = VisitorCollection.AsQueryable().Take(request.count).OrderBy(x => x.CreateDateTime)
            .Select(x => new GetLatestVisitorWebsiteInfoDto()
            {
                Device = x.Device.Family,
                PhysicalPath = x.PhysicalPath,
                CurrentLink = x.CurrentLink,
                Browser = x.Browser.Family,
                Id = x.Id,
                OperationSystem = x.OperationSystem.Family,
                RefererLink = x.RefererLink,
                Ip = x.Ip,
                IsSpider = x.Device.IsSpider,
                CreateDateTime = x.CreateDateTime,
                VisitorId = x.VisitorId
            }).ToList();
      return new ResponseGetLatestVisitorWebsiteInfo()
      {
          GetLatestVisitors = result
      };
    }
}
