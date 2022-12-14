using ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogTypeService catalogTypeService;

        public IndexModel(ICatalogTypeService  catalogTypeService)
        {
            this.catalogTypeService = catalogTypeService;
            CatalogType = new List<CatalogTypeListDto>();
        }

        public List<CatalogTypeListDto> CatalogType { get; set; } 
        public async Task  OnGet(int? parentId, int page = 1, int pageSize = 100)
        {
            var result =  await catalogTypeService.GetAsync(parentId, page, pageSize);
            if (result.IsSuccess) CatalogType = result.Data;
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
