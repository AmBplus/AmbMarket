using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Endpoint.Models.ViewModels.Catalogs;
using ambMarket.Application.Services.Catalogs.CatalogItems.AddNewCatalogItem;
using ambMarket.Application.Services.Catalogs.CatalogItems.CatalogItemServices;
using ambMarket.Infrastructure.ExternalApi.ImageServer;
using Mapster;
//using Application.Catalogs.CatalohItems.AddNewCatalogItem;
using MediatR;
//using Application.Dtos;
//using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Dto;

namespace Admin.EndPoint.Pages.CatalogItems
{
    public class CreateModel : PageModel
    {
        private IMediator Mediator { get; }
        // private readonly IImageUploadService imageUploadService;
        private ISaveNewCatalogItemQueryService catalogItemService { get; }
        private IImageUploadService IimageUploadService { get; }
        public CreateModel(IMediator mediator, ISaveNewCatalogItemQueryService catalogItemService, IImageUploadService iimageUploadService)
        {
            Mediator = mediator;
            this.catalogItemService = catalogItemService;
            IimageUploadService = iimageUploadService;
        }

        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }

        [BindProperty]
        public CatalogItemViewModel Data { get; set; }
        [BindProperty]
        public List<IFormFile> Files { get; set; }


        public void OnGet()
        {
            Categories = new SelectList(catalogItemService.GetCatalogType(), "Id", "Type");
            Brands = new SelectList(catalogItemService.GetBrand(), "Id", "Brand");
        }



        public async Task<JsonResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors).Select(x=>x.ErrorMessage).ToList();
                
                return new JsonResult(ResultDto<int>.BuildFailedResult(allErrors,
                    0));
            }
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                Files.Add(file);
            }
            List<AddNewCatalogItemImage_Dto> images = new List<AddNewCatalogItemImage_Dto>();
            if (Files.Count > 0)
            {
                //Upload 
                var result = await IimageUploadService.Upload(Files);
                foreach (var item in result)
                {
                    images.Add(new AddNewCatalogItemImage_Dto { Src = item });
                }
            }
            var requestSaveCatalogItemDto =  Data.Adapt<RequestSaveNewCatalogItemDto>();
            requestSaveCatalogItemDto.Images = images;
            var resultService = await Mediator.Send(requestSaveCatalogItemDto);
            return new JsonResult(resultService);
        }
    }
}
