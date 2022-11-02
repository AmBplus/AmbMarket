using ambMarket.Application.Interfaces.UriComposer;
using ambMarket.Application.Services.Catalogs.CatalogItems.Cmd.AddNewCatalogItem;
using ambMarket.Application.Services.Catalogs.CatalogItems.Query.CatalogItemServices.GetSaveNewCatalogItemQuery;
using ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;
using ambMarket.Domain.CatalogAggregate;
using ambMarket.Infrastructure.Settings;
using ambMarket.Infrastructure.Utilities.UriCompose;
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
        // Brand
        CreateMap<CatalogBrand, ListCatalogBrandDto>()
            .ForMember(des => des.Brand,opt=> opt.MapFrom(src => src.Name))
            .ReverseMap();
        // Catalog Type
        CreateMap<CatalogType, CatalogTypeDto>()
            .ReverseMap();
        // CatalogImage
        CreateMap<CatalogItemImage, AddNewCatalogItemImage_Dto>()
            .ForMember(des=>
                des.Src, opt
                =>opt.MapFrom(x=>x.Src))
            .ReverseMap();
        // Catalog Features
        CreateMap<CatalogItemFeature, AddNewCatalogItemFeature_dto>()
            .ReverseMap();
        // CatalogItem
        CreateMap<CatalogItem, RequestSaveNewCatalogItemCmd>()
            .ForMember(des=>des.Name,opt=>opt.MapFrom(x=>x.Name))
            .ForMember(des=>des.Features,opt=>opt.MapFrom(x=>x.CatalogItemFeatures))
            .ForMember(des=>des.Images,opt=>opt.MapFrom(x=>x.CatalogItemImages))
            .ForMember(des=>des.AvailableStock,opt=>opt.MapFrom(x=>x.AvailabeStock))
            .ForMember(des=>des.MaxStockThreshold,opt=>opt.MapFrom(x=>x.MaxStockThreshold))
            .ForMember(des=>des.RestockThreshold,opt=>opt.MapFrom(x=>x.RentStock))
            .ReverseMap();
    }

}