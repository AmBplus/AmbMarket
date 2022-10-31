using ambMarket.Application.Services.Catalogs.CatalogBrandService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogBrand
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogBrandRepositoryService catalogBrandService;

        public IndexModel(ICatalogBrandRepositoryService  catalogBrandService)
        {
            this.catalogBrandService = catalogBrandService;
            CatalogBrand = new List<CatalogBrandDto>();
        }

        public List<CatalogBrandDto> CatalogBrand { get; set; } 
        public async Task  OnGet( int page = 1, int pageSize = 100)
        {
            var result =  await catalogBrandService.GetAsync(page, pageSize);
            if (result.IsSuccess) CatalogBrand = result.Data;
            else
            {
                foreach (var error in result.Message)
                {
                    ModelState.AddModelError("", error);
                }
            }
           
        }
    }
}
