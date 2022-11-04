using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.CatalogAggregate;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Dto;
using Shared.Resources;

namespace ambMarket.Application.Services.Catalogs.CatalogBrandCrudService;

public class CatalogBrandService : ICatalogBrandService
{
    private IMarketDbContext Context { get; }
    public CatalogBrandService(IMarketDbContext context)
    {
        Context = context;
    }
    public async Task<ResultDto<List<CatalogBrandDto>>> GetAsync()
    {
        var result = await Context.CatalogBrands
            .ProjectToType<CatalogBrandDto>()
            .ToListAsync();
        return Utility.GenerateResultDto(result);
    }

    public async Task<ResultDto<CatalogBrandDto>> GetAsync(int id)
    {
        var catalogType = await Context.CatalogBrands
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        var result = catalogType.Adapt<CatalogBrandDto>();
        return Utility.GenerateResultDto(result);
    }

    public async Task<ResultDto<List<CatalogBrandDto>>> GetAsync(int page = 1, int pageSize = Constants.Page.PageSize)
    {
        var result = await Context.CatalogBrands
            .ProjectToType<CatalogBrandDto>()
            .ToPaged(page, pageSize).ToListAsync();
        return Utility.GenerateResultDto(result);
    }

    public async Task<ResultDto<int>> Save(CatalogBrandDto catalogBrandDto)
    {
        var catalogBrand = catalogBrandDto.Adapt<CatalogBrand>();
        await Context.CatalogBrands.AddAsync(catalogBrand);
        await SaveChangesAsync();
        return Utility.GenerateResultDto<int>(catalogBrand.Id);
    }

    public async Task<ResultDto> Remove(int id)
    {
        var catalogBrand = await Context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == id);
        if (catalogBrand == null) return ResultDto.BuildFailedResult(ErrorMessages.NotFind);
        catalogBrand.SetRemove();

        Context.CatalogBrands.Update(catalogBrand);
        await SaveChangesAsync();
        return ResultDto.BuildSuccessResult();
    }

    public async Task<ResultDto> Update(CatalogBrandDto catalogBrandDto)
    {
        var catalogBrand = await Context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == catalogBrandDto.Id);
        if (catalogBrand == null) return ResultDto.BuildFailedResult(ErrorMessages.NotFind);
        catalogBrand.Name = catalogBrandDto.Name;
        catalogBrand.SetUpdateDate();
        Context.CatalogBrands.Update(catalogBrand);
        await SaveChangesAsync();
        return ResultDto.BuildSuccessResult();
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

}