using Shared.Dto;

namespace ambMarket.Application.Services.Catalogs.CatalogBrandCrudService;

public interface ICatalogBrandService
{
    Task<ResultDto<List<CatalogBrandDto>>> GetAsync();
    Task<ResultDto<CatalogBrandDto>> GetAsync(int id);
    Task<ResultDto<List<CatalogBrandDto>>> GetAsync(int page = 1, int pageSize = Shared.Constants.Page.PageSize);
    Task<ResultDto<int>> Save(CatalogBrandDto catalogTypeDto);
    Task<ResultDto> Remove(int id);
    Task<ResultDto> Update(CatalogBrandDto catalogBrandDto);
    public void SaveChanges();
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}