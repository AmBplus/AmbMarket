using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _02_Framework.Application.Interfaces.DatabaseContext;

public interface IBaseDbContext
{
    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken());

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity :class ;
    EntityEntry Entry(object entity);

   
}