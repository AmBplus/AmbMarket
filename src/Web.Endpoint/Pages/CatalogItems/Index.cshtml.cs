using ambMarket.Application.Dto;
using ambMarket.Application.Services.Catalogs.CatalogItems.Query.GetPLP;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared;

namespace Web.Endpoint.Pages.CatalogItems
{
    public class IndexModel : PageModel
    {
        public IndexModel(IMediator mediator)
        {
            Mediator = mediator;
        }
        public ResponseGetPlp Response { get; set; }
        private IMediator Mediator { get; }
        public async Task OnGet(int pageNumber = 1 , int pageSize = Constants.Page.PageSize)
        {
            Response = await Mediator.Send(new RequestGetPLP()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (Response.Result.Data == null)
            {
                Response.Result.Data = new List<GetPlpDto>();
            }
        }
    }
}
