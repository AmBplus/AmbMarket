using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    public class CreateModel : PageModel
    {
        private readonly ICatalogTypeService catalogTypeService;

        public CreateModel(ICatalogTypeService catalogTypeService)
        {
            this.catalogTypeService = catalogTypeService;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel { };
        public List<string> Message { get; set; } = new List<string>();

        public void OnGet(int? parentId)
        {
            CatalogType.ParentId = parentId;
        }


        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await catalogTypeService.Save(CatalogType.Adapt<CatalogTypeDto>());
            if (result.IsSuccess)
            {
                return RedirectToPage("index");
            }
            Message = result.Message;
            return Page();
        }
    }
}
