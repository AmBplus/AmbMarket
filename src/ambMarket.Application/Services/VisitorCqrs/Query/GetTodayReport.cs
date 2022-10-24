using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.Visitors;
using MediatR;
using MongoDB.Driver;
using Shared;
using System.Linq;

namespace ambMarket.Application.Services.VisitorCqrs.Query;

public class RequestGetVisitorTodayReport : IRequest<ResponseGetVisitorTodayReport>
{
    
}

public class ResponseGetVisitorTodayReport
{
    public GeneralVisitStateDto GeneralVisitStateDto { get; set; }
    public TodayVisitDto TodayVisitDto { get; set; }
}

public class GetVisitorTodayReportHandler : IRequestHandler<RequestGetVisitorTodayReport, ResponseGetVisitorTodayReport>
{
    private IMongoCollection<Visitor> VisitorCollection { get; }
    public GetVisitorTodayReportHandler(IMongoDbContext<Visitor> mongoDbContext)
    {
        VisitorCollection = mongoDbContext.GetCollection();
    }
    public async Task<ResponseGetVisitorTodayReport> Handle(RequestGetVisitorTodayReport request, CancellationToken cancellationToken)
    {
        var start = Utility.Now.Date;
        var end = Utility.Now.AddDays(1);
        var pageViewCountQuery = VisitorCollection.AsQueryable()
            .Where(x => x.CreateDateTime >= start && x.CreateDateTime <= end);
        var todayPageViewCount =pageViewCountQuery.LongCount();
        var todayVisitorCount = pageViewCountQuery.GroupBy(x => x.VisitorId).LongCount();
        var totalPageViews = VisitorCollection.AsQueryable().LongCount();
        var totalVisitorCount = VisitorCollection.AsQueryable().LongCount();
        return new ResponseGetVisitorTodayReport()
        {
            GeneralVisitStateDto = new GeneralVisitStateDto()
            {
                TotalVisitors = totalVisitorCount,
                TotalPageViews = totalPageViews,
                TotalVisitorCount = totalVisitorCount,
                PageViewsPerVisit = totalPageViews/totalVisitorCount,
            },
            TodayVisitDto = new TodayVisitDto()
            {
                PageViews = todayPageViewCount,
                Visitors = todayVisitorCount,
            }
        };
    }
}

public class GeneralVisitStateDto
{
    public long TotalPageViews { get; set; }
    public long TotalVisitors { get; set; }
    public long PageViewsPerVisit { get; set; }
    public long TotalVisitorCount { get; set; }
}

public class TodayVisitDto
{
    public long PageViews { get; set; }
    public long Visitors { get; set; }
}