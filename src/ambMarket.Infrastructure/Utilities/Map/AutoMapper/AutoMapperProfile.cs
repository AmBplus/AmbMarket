using ambMarket.Application.Services.Catalogs.CatalogItems.AddNewCatalogItem;
using ambMarket.Application.Services.Catalogs.CatalogItems.CatalogItemServices;
using ambMarket.Application.Services.Catalogs.CatalogTypeService;
using ambMarket.Domain.Catalog;
using AutoMapper;

namespace ambMarket.Infrastructure.Utilities.Map.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
       MapCatalogItem();
    }

    private void MapCatalogItem()
    {
        CreateMap<CatalogBrand, Application.Services.Catalogs.CatalogItems.CatalogItemServices.ListCatalogBrandDto>()
            .ForMember(des => des.Brand,opt=> opt.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<CatalogType, Application.Services.Catalogs.CatalogTypeService.CatalogTypeDto>()
            .ReverseMap();
        CreateMap<CatalogItemImage, AddNewCatalogItemImage_Dto>()
            .ForMember(des=>des.Src,opt=>opt.MapFrom(x=>x.Src))
            .ReverseMap();
        CreateMap<CatalogItemFeature, AddNewCatalogItemFeature_dto>()
            .ReverseMap();
        CreateMap<CatalogItem, RequestSaveNewCatalogItemDto>()
            .ForMember(des=>des.Name,opt=>opt.MapFrom(x=>x.Name))
            .ForMember(des=>des.Features,opt=>opt.MapFrom(x=>x.CatalogItemFeatures))
            .ForMember(des=>des.Images,opt=>opt.MapFrom(x=>x.CatalogItemImages))
            .ForMember(des=>des.AvailableStock,opt=>opt.MapFrom(x=>x.AvailabeStock))
            .ForMember(des=>des.MaxStockThreshold,opt=>opt.MapFrom(x=>x.MaxStockThreshold))
            .ForMember(des=>des.RestockThreshold,opt=>opt.MapFrom(x=>x.RentStock))
            .ReverseMap();
    }

}