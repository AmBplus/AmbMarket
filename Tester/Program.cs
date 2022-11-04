// See https://aka.ms/new-console-template for more information

using Mapster;

Console.WriteLine("Hello, World!");

CatalogItemDto catalogItemDto = new CatalogItemDto()
{
    CatalogItemDtoName = "CatalogItemDtoName"
};
BasketItemDto basketItemDto = new BasketItemDto()
{
    CatalogItem = catalogItemDto,
    BasketName = "BasketName",
};
BasketDto basketDto = new BasketDto()
{
    BasketItem = basketItemDto,
    Name = nameof(BasketDto)
};
TypeAdapterConfig<CatalogItem, CatalogItemDto>
    .NewConfig()
    .TwoWays()
    .Map(des => des.CatalogItemDtoName, src => src.CatalogItemName);
var catalogItem = catalogItemDto.Adapt<CatalogItem>()
    ;
var result = basketDto.Adapt<Basket>();
Console.ReadKey();
public class Basket
{
    public string Name { get; set; }
    public BasketItem BasketItem { get; set; }
}

public class BasketItem
{
    public string BasketName { get; set; }
    public CatalogItem CatalogItem { get; set; }
}

public class CatalogItem
{
    public string CatalogItemName { get; set; }
}
public class BasketDto
{
    public string Name { get; set; }
    public BasketItemDto BasketItem { get; set; }
}

public class BasketItemDto
{
    public string BasketName { get; set; }
    public CatalogItemDto CatalogItem { get; set; }
}

public class CatalogItemDto
{
    public string CatalogItemDtoName { get; set; }
}