using Shared.Dto;

namespace ambMarket.Application.Services.Catalogs;

public interface ICatalogTypeRepositoryService
{
    ResultDto<CatalogTypeDto> Get();
}

public class CatalogTypeDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int?ParentCatalogTypeId { get; set; }
}