using ambMarket.Application.Services.VisitorCqrs.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.Visitors
{
    public class IndexModel : PageModel
    {
        public IndexModel(IMediator mediator)
        {
            Mediator = mediator;
        }

        public ResponseGetVisitorTodayReport ResultTodayReport { get; set; }
        public ResponseGetLatestVisitorWebsiteInfo GetLatestVisitorsWebsite { get; set; }
        private IMediator Mediator { get; }
        public async void OnGet()
        {
             ResultTodayReport = await Mediator.Send(new RequestGetVisitorTodayReport());
             GetLatestVisitorsWebsite = await Mediator.Send(new RequestGetLatestVisitorWebsiteInfo
             {
                 count = 10
             });

        }
    }
}
