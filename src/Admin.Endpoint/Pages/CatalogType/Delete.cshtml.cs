using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs;
using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    public class DeleteModel : PageModel
    {
        private readonly ICatalogTypeRepositoryService catalogTypeService;

        public DeleteModel(ICatalogTypeRepositoryService catalogTypeService)
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
            var result = await catalogTypeService.Remove(CatalogType.Id);
            Message = result.Message.ToList();
            if (result.IsSuccess)
            {
                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
