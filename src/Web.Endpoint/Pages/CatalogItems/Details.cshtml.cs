using ambMarket.Application.Services.Catalogs.CatalogItems.Query.GetPdp;
using ambMarket.Infrastructure.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Endpoint.Pages.CatalogItems
{
    public class DetailsModel : PageModel
    {
        private IMediator Mediator { get; }
        public DetailsModel(IMediator mediator)
        {
            Mediator = mediator;
        }
        public GetPdpDto Pdp { get; set; }
        public async Task<IActionResult> OnGet(long id)
        {
            var result = await Mediator.Send(new RequestGetPdp() { Id = id });
            if (result.IsSuccess)
            {
                Pdp = result.Data;
                return Page();
            }

            return RedirectToPage("/Error", new { message = ErrorType._404 });
        }
    }
}
