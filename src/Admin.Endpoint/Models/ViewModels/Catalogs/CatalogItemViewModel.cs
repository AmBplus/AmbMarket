using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Admin.Endpoint.Models.ViewModels.Catalogs;

public class CatalogItemViewModel
{
    [Required(ErrorMessage = "نام ضرویست")]
    public string Name { get; set; }
    [Required(ErrorMessage = "توضیحات ضرویست")]
    //[DataType(DataType.MultilineText)]
    public string Description { get; set; }
    [Required(ErrorMessage = "قیمت ضرویست")]
    [Range(0,int.MaxValue,ErrorMessage = "رنج قیمت نامعتبر است")]
    public int Price { get; set; }
    [Required(ErrorMessage = "کاتالوگ آیدی ضرویست")]
    [Range(0, int.MaxValue, ErrorMessage = "رنج کاتالوگ آیدی نامعتبر است")]
    public int CatalogTypeId { get; set; }
    [Required(ErrorMessage = " کاتالوگ برند آیدی ضرویست")]
    [Range(0, int.MaxValue, ErrorMessage = "رنج کاتالوگ برند آیدی نامعتبر است")]
    public int CatalogBrandId { get; set; }
    [Required(ErrorMessage = "تعداد موجودی ضرویست")]
    [Range(0, int.MaxValue, ErrorMessage = "رنج تعداد موجودی نامعتبر است")]
    public int AvailableStock { get; set; }
    [Required(ErrorMessage = "حداقل برای سفارش مجدد ضرویست")]
    [Range(0, int.MaxValue, ErrorMessage = "رنج حداقل برای سفارش مجدد نامعتبر است")]
    public int RestockThreshold { get; set; }
    [Required(ErrorMessage = "حداکثر توان ذخیره ضرویست")]
    [Range(0, int.MaxValue, ErrorMessage = "رنج حداکثر توان ذخیره در انبار نامعتبر است")]
    public int MaxStockThreshold { get; set; }
    [ValidateNever]
    public List<CatalogItemFeatureViewModel_dto> Features { get; set; }
}

public class CatalogItemFeatureViewModel_dto
{
   // [Required(ErrorMessage = "اسم مقدار ضررویست")]
    public string Key { get; set; }
   // [Required(ErrorMessage = "مقدار ضرویست")]
    public string Value { get; set; }
   // [Required(ErrorMessage = "گروه ضروریست")]
    public string Group { get; set; }
}
