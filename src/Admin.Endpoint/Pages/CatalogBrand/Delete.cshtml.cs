using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs.CatalogBrandCrudService;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogBrand
{
    public class DeleteModel : PageModel
    {
        private readonly ICatalogBrandRepositoryService catalogBrandService;

        public DeleteModel(ICatalogBrandRepositoryService catalogBrandService)
        {
            this.catalogBrandService = catalogBrandService;
        }

        [BindProperty]
        public CatalogBrandViewModel CatalogBrand { get; set; } = new CatalogBrandViewModel();
        public List<String> Message { get; set; } = new List<string>();
        public async Task OnGet(int Id)
        {
            var model = await catalogBrandService.GetAsync(Id);
            if (model.IsSuccess)
                CatalogBrand = model.Data.Adapt<CatalogBrandViewModel>();
             Message = model.Message.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await catalogBrandService.Remove(CatalogBrand.Id);
            Message = result.Message.ToList();
            if (result.IsSuccess)
            {
                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
