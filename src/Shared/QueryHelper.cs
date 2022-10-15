using System.Linq.Expressions;

namespace Shared;

public static class QueryHelper
{
    public static IQueryable<T> QuerySearchGenerator<T>(this IQueryable<T> entity, string? SearchKey, Expression<Func<T, bool>> command)
    {
        if (!string.IsNullOrWhiteSpace(SearchKey))
        {
            entity = entity.Where(command);
        }
        return entity;
    }
}