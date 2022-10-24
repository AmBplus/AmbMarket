using _01_Framework.Domain.Execeptions;
using _02_Framework.Application.Interfaces.DatabaseContext;
using MongoDB.Driver;

namespace ambMarket.Persistence.Contexts.MongoDb;

public class MongoDbContext<T> : IMongoDbContext<T>
{
    private IMongoCollection<T> collection { get; }

    public MongoDbContext(MongoConnectionSettings mongoConnectionSettings)
    {
        CheckValidConnectionSetting(mongoConnectionSettings);
       
        var client = (!string.IsNullOrWhiteSpace(mongoConnectionSettings.ConnectionString)) ? 
            new MongoClient(MongoUrl.Create(mongoConnectionSettings.ConnectionString))
            : new MongoClient();
        var db = client.GetDatabase(mongoConnectionSettings.DatabaseName);
        collection = db.GetCollection<T>(typeof(T).Name);
    }

    private void CheckValidConnectionSetting( MongoConnectionSettings mongoConnectionSettings)
    {
        if (string.IsNullOrWhiteSpace(mongoConnectionSettings.DatabaseName) )
        {
            throw new InvalidMongoDbConnectionSettings();
        }
    }

    public IMongoCollection<T> GetCollection()
    {
        return collection;
    }
}