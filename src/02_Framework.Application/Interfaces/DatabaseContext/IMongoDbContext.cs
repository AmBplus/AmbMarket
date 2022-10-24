using MongoDB.Driver;

namespace _02_Framework.Application.Interfaces.DatabaseContext;

public interface IMongoDbContext<T>
{
    public IMongoCollection<T> GetCollection();
}