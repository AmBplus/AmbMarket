using MongoDB.Driver;

namespace _02_Framework.Application.Interfaces.Repositories;

public interface IMongoDbRepository<T>
{
    public void Add(T entity);
    public T Get(Guid id);
    public List<T> Get();
    public void Delete(Guid id);
    public void Update(UpdateDefinition<T> update, Guid id);
    public void Update(UpdateDefinition<T> update, FilterDefinition<T> filter);
    public IMongoCollection<T> GetCollection();
}