using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    public class EditModel : PageModel
    {
        private readonly ICatalogTypeService catalogTypeService;

        public EditModel(ICatalogTypeService catalogTypeService)
        {
            this.catalogTypeService = catalogTypeService;
        }


        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel();
        public List<String> Message { get; set; } = new List<string>();
      
        public async Task OnGet(int Id)
        {
            var model = await catalogTypeService.GetAsync(Id);
            if (model.IsSuccess)
                CatalogType = model.Data.Adapt<CatalogTypeViewModel>();
            Message = model.Message.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await catalogTypeService.Update(CatalogType.Adapt<CatalogTypeDto>());
            Message =  result.Message.ToList();
            return RedirectToPage("Index");
        }
    }
}
