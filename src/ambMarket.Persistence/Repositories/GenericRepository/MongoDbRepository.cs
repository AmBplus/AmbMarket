using _02_Framework.Application.Interfaces.DatabaseContext;
using _02_Framework.Application.Interfaces.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ambMarket.Persistence.Repositories.GenericRepository;

public class MongoDbRepository<T> : IMongoDbRepository<T>
{
    public MongoDbRepository(IMongoDbContext<T> context)
    {
        collection = context.GetCollection();
    }
    private IMongoCollection<T> collection { get; }
    public void Add(T entity)
    {
        collection.InsertOne(entity);
    }
    public T Get(Guid id)
    {
        return collection.Find(GetFilterById(id)).FirstOrDefault();
    }
    public List<T> Get()
    {
        return collection.Find(new BsonDocument()).ToList();
    }

    public void Delete(Guid id)
    {
        collection.DeleteOne(GetFilterById(id));
    }

    public void Update(UpdateDefinition<T> update, Guid id)
    {
        collection.UpdateOne(GetFilterById(id), update);
    }
    public void Update(UpdateDefinition<T> update, FilterDefinition<T> filter)
    {
        collection.UpdateOne(filter, update);
    }

    public IMongoCollection<T> GetCollection()
    {
        return collection;
    }
    private FilterDefinition<T>? GetFilterById(Guid id)
    {
        return Builders<T>.Filter.Eq("Id", id);
    }
}