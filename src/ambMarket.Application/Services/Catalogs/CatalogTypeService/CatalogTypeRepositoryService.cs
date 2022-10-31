using ambMarket.Application.Interfaces.Databases;
using ambMarket.Domain.Catalog;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Dto;
using Shared.Resources;

namespace ambMarket.Application.Services.Catalogs.CatalogTypeService;

public class CatalogTypeRepositoryService : ICatalogTypeRepositoryService
{
    private IMarketDbContext Context { get; }
    public CatalogTypeRepositoryService(IMarketDbContext context)
    {
        Context = context;
    }
    public async Task<ResultDto<List<CatalogTypeListDto>>> GetAsync()
    {

        var result = await Context.CatalogTypes.Include(x => x.Childs)
            .MapCatalogTypeToCatalogTypeListDtos()
            .ToListAsync();
        return Utility.GenerateResultDto(result);
    }

    public async Task<ResultDto<CatalogTypeDto>> GetAsync(int id)
    {
        var catalogType = await Context.CatalogTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
        var result = catalogType.Adapt<CatalogTypeDto>();
        return Utility.GenerateResultDto(result);
    }

    public async Task<ResultDto<List<CatalogTypeListDto>>> GetAsync(int? parentId, int page = 1, int pageSize = Constants.Page.PageSize)
    {
        var result = await Context.CatalogTypes.Where(x => x.ParentId == parentId)
            .ToPaged(page, pageSize)
            .MapCatalogTypeToCatalogTypeListDtos().ToListAsync();
        return Utility.GenerateResultDto(result);
    }

    public async Task<ResultDto<int>> Save(CatalogTypeDto catalogTypeDto)
    {
        var catalogType = catalogTypeDto.Adapt<CatalogType>();
        await Context.CatalogTypes.AddAsync(catalogType);
        await SaveChangesAsync();
        return Utility.GenerateResultDto(catalogType.Id);
    }

    public async Task<ResultDto> Remove(int id)
    {
        var catalogType = await Context.CatalogTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (catalogType == null) return ResultDto.BuildFailedResult(ErrorMessages.NotFind);
        catalogType.SetRemove();

        Context.CatalogTypes.Update(catalogType);
        await SaveChangesAsync();
        return ResultDto.BuildSuccessResult();
    }

    public async Task<ResultDto> Update(CatalogTypeDto catalogTypeDto)
    {
        var catalogType = await Context.CatalogTypes.FirstOrDefaultAsync(x => x.Id == catalogTypeDto.Id);
        if (catalogType == null) return ResultDto.BuildFailedResult(ErrorMessages.NotFind);
        catalogType.ParentId = catalogTypeDto.ParentId;
        catalogType.Type = catalogTypeDto.Type;
        catalogType.SetUpdateDate();
        Context.CatalogTypes.Update(catalogType);
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