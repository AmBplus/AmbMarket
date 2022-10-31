using FluentValidation;
using MediatR;
using Shared.Dto;
using Shared.Resources;

namespace ambMarket.Application.Services.Catalogs.CatalogItems.AddNewCatalogItem
{
    public class RequestSaveNewCatalogItemDto : IRequest<ResultDto<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public List<AddNewCatalogItemFeature_dto> Features { get; set; }
        public List<AddNewCatalogItemImage_Dto> Images { get; set; }

    }

    public class AddNewCatalogItemFeature_dto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
    }

    public class AddNewCatalogItemImage_Dto
    {
        public string Src { get; set; }
    }
    public class AddNewCatalogItemDtoValidator: AbstractValidator<RequestSaveNewCatalogItemDto>
    {
        public AddNewCatalogItemDtoValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("نام کاتالوگ اجباری است");
            RuleFor(x => x.Name).Length(2, 100).WithMessage(ErrorFluentValidation.BetweenMinMax);
            RuleFor(x => x.Description).NotNull().WithMessage("توضیحات نمی تواند خالی باشد");
            RuleFor(x => x.AvailableStock).InclusiveBetween(0, int.MaxValue).WithMessage(ErrorFluentValidation.BetweenMinMaxWithEnteredNumber);
            RuleFor(x => x.Price).InclusiveBetween(0, int.MaxValue).WithMessage(ErrorFluentValidation.BetweenMinMaxWithEnteredNumber);
            RuleFor(x => x.Price).NotNull().WithMessage(ErrorFluentValidation.Null);
            RuleForEach(x => x.Features).SetValidator(new AddNewCatalogItemFeature_dto_Validator());
            RuleForEach(x => x.Images).SetValidator(new AddNewCatalogItemImage_Dto_Validator());
        }
    }

    public class AddNewCatalogItemImage_Dto_Validator : AbstractValidator<AddNewCatalogItemImage_Dto>
    {
        public AddNewCatalogItemImage_Dto_Validator()
        {
            RuleFor(x => x.Src).NotEmpty().WithMessage(ErrorFluentValidation.EmptyOrWhiteSpace);
        }
    }

    public class AddNewCatalogItemFeature_dto_Validator : AbstractValidator<AddNewCatalogItemFeature_dto>
    {
        public AddNewCatalogItemFeature_dto_Validator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage(ErrorFluentValidation.EmptyOrWhiteSpace);
            RuleFor(x => x.Group).NotEmpty().WithMessage(ErrorFluentValidation.EmptyOrWhiteSpace);
            RuleFor(x => x.Key).NotEmpty().WithMessage(ErrorFluentValidation.EmptyOrWhiteSpace);
        }
    }
}
