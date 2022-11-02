using ambMarket.Application.Services.Catalogs.CatalogItems.Cmd.RemoveCatalogItem;
using ambMarket.Application.Services.Catalogs.CatalogItems.Cmd.UnRemoveCatalogItem;
using ambMarket.Application.Services.Catalogs.CatalogItems.Query.CatalogItemServices.GetListCatalogForAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared;

namespace Admin.Endpoint.Pages.CatalogItems
{
    public class IndexModel : PageModel
    {
        public IndexModel(IMediator mediator)
        {
            Mediator = mediator;
        }
        public ResponseGetListCatalogItemForAdmin Result { get; set; }
        private IMediator Mediator { get; }
        public async Task OnGet(int page = 1, int pageSize = Constants.Page.PageSize)
        {
            Result =await Mediator.Send(new RequestGetListCatalogItemForAdmin()
            {
                PageNumber = page,
                PageSize = pageSize
            });
        }

        public async Task<IActionResult> OnGetDelete(long id)
        {
          var response= await  Mediator.Send(new RequestRemoveCatalogItemService { Id = id });
          if (response.Result.IsSuccess)
          {
              return RedirectToPage();
          }
          return Page();
        }
        public async Task<IActionResult> OnGetUnDelete(long id)
        {
            var response = await Mediator.Send(new RequestUnRemoveCatalogItemService { Id = id });
            if (response.Result.IsSuccess)
            {
                return RedirectToPage();
            }

            return Page();
        }
    }
}
