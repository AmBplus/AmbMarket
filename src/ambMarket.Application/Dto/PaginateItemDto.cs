namespace ambMarket.Application.Dto;

public class PaginateItemDto<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public T Data { get; set; }
}