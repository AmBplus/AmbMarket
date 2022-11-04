using Shared.Dto;

namespace ambMarket.Application.Services.Catalogs.CatalogTypeCrudService;

public interface ICatalogTypeService
{
    Task<ResultDto<List<CatalogTypeListDto>>> GetAsync();
    Task<ResultDto<CatalogTypeDto>> GetAsync(int id);
    Task<ResultDto<List<CatalogTypeListDto>>> GetAsync(int? parentId, int page = 1, int pageSize = Shared.Constants.Page.PageSize);
    Task<ResultDto<int>> Save(CatalogTypeDto catalogTypeDto);
    Task<ResultDto> Remove(int id);
    Task<ResultDto> Update(CatalogTypeDto catalogTypeDto);
    public void SaveChanges();
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}