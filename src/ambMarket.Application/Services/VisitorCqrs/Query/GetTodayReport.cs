using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.Visitors;
using MediatR;
using MongoDB.Driver;
using Shared;

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
    private DateTime StartDay { get;  }
    private DateTime EndDay { get; }
    private DateTime StartMonth { get; }
    private DateTime EndMonth { get; }
    public GetVisitorTodayReportHandler(IMongoDbContext<Visitor> mongoDbContext)
    {
        VisitorCollection = mongoDbContext.GetCollection();
        StartDay = Utility.Now.Date; 
        EndDay = Utility.Now.Date.AddDays(1);
        StartMonth = Utility.Now.Date.Date.AddDays(-29);
        EndMonth = Utility.Now.Date.AddDays(1);
    }
    public async Task<ResponseGetVisitorTodayReport> Handle(RequestGetVisitorTodayReport request, CancellationToken cancellationToken)
    {
        return new ResponseGetVisitorTodayReport()
        {
            GeneralVisitStateDto = GeneralVisitStateDto(),
            TodayVisitDto = GetTodayVisitDto()
        };
    }

    private GeneralVisitStateDto GeneralVisitStateDto()
    {
        var totalVisitorCount = VisitorCollection.AsQueryable().GroupBy(x => x.VisitorId).LongCount();
        var totalVisit = VisitorCollection.AsQueryable().LongCount();
        return new GeneralVisitStateDto()
        {
            TotalVisitorsCount = totalVisitorCount,
            TotalView = totalVisit,
            PageViewsPerVisitor = (totalVisitorCount == 0) ? 0 : totalVisit / totalVisitorCount,
            TotalViewsPerPage = VisitorCollection.AsQueryable().GroupBy(x => x.PhysicalPath).LongCount(),
            VisitPerMonth = GetVisitPerMonth(),
        };
    }
    private TodayVisitDto GetTodayVisitDto()
    {
        var pageViewCountQuery = VisitorCollection.AsQueryable()
            .Where(x => x.CreateDateTime >= StartDay  && x.CreateDateTime <= EndDay);
        var todayPageViewCount = pageViewCountQuery.GroupBy(x => x.PhysicalPath).LongCount();
        var todayVisitorCount = pageViewCountQuery.GroupBy(x => x.VisitorId).LongCount();
        return new TodayVisitDto()
        {
            TotalViewsPerPage = todayPageViewCount,
            VisitorsCount = todayVisitorCount,
            TotalView = pageViewCountQuery.LongCount(),
            VisitPerHour = GetVisitPerHour(),
        };
    }
    private VisitCountDto GetVisitPerHour()
    {
        var TodayPageViewList = VisitorCollection.AsQueryable()
            .Where(x => x.CreateDateTime > StartDay && x.CreateDateTime < EndDay)
            .Select(x => new { x.CreateDateTime }).ToList();
        VisitCountDto visitPerHour = new VisitCountDto()
        {
            Display = new string[24],
            Values = new int[24]
        };
        for (int i = 0; i <= 23; i++)
        {
            visitPerHour.Display[i] =  $"{i}";
            visitPerHour.Values[i] = TodayPageViewList.Where(x=>x.CreateDateTime.Hour == i).Count();
        }
        return visitPerHour;
    }

    private VisitCountDto GetVisitPerMonth()
    {
        var monthViewList = VisitorCollection.AsQueryable()
            .Where(x => x.CreateDateTime > StartMonth && x.CreateDateTime < EndMonth)
            .Select(x => new { x.CreateDateTime }).ToList();
        VisitCountDto perMonth = new VisitCountDto()
        {
            Display = new string[30],
            Values = new int[30]
        };
        for (int i = 0; i < 30; i++)
        {
            var currentDate = Utility.Now.AddDays(i * (-1)); 
            perMonth.Display[i] = $"{i}";
            perMonth.Values[i] = monthViewList.Where(x => x.CreateDateTime.Hour == i).Count();
        }
        return perMonth;
    }
}

public class GeneralVisitStateDto
{
    public long TotalVisitorsCount { get; set; }
    public long PageViewsPerVisitor { get; set; }
    public long TotalView { get; set; }
    public long TotalViewsPerPage { get; set; }
    public VisitCountDto VisitPerMonth { get; set; }
}

public class TodayVisitDto
{
    public long TotalViewsPerPage { get; set; }
    public long VisitorsCount { get; set; }
    public long TotalView { get; set; }
    public VisitCountDto VisitPerHour { get; set; }
}

public class VisitCountDto
{
    public string[] Display { get; set; }
    public int[] Values { get; set; }
}