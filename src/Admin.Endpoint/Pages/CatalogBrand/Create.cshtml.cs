using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs.CatalogBrandCrudService;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogBrand
{
    public class CreateModel : PageModel
    {
        private readonly ICatalogBrandRepositoryService catalogBrandService;

        public CreateModel(ICatalogBrandRepositoryService catalogBrandService)
        {
            this.catalogBrandService = catalogBrandService;
        }

        [BindProperty]
        public CatalogBrandViewModel CatalogBrand { get; set; } = new CatalogBrandViewModel { };
        public List<string> Message { get; set; } = new List<string>();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await catalogBrandService.Save(CatalogBrand.Adapt<CatalogBrandDto>());
            if (result.IsSuccess)
            {
                return RedirectToPage("index");
            }
            Message = result.Message;
            return Page();
        }
    }
}
