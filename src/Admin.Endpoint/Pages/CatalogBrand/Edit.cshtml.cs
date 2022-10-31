using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs.CatalogBrandService;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogBrand
{
    public class EditModel : PageModel
    {
        private readonly ICatalogBrandRepositoryService catalogBrandService;

        public EditModel(ICatalogBrandRepositoryService catalogBrandService)
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
            var result = await catalogBrandService.Update(CatalogBrand.Adapt<CatalogBrandDto>());
            Message =  result.Message.ToList();
            return RedirectToPage("Index");
        }
    }
}
