namespace ambMarket.Persistence.Contexts.MongoDb;

public class MongoConnectionSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }

    public MongoConnectionSettings(string databaseName)
    {
        DatabaseName = databaseName;
    }
    public MongoConnectionSettings(string databaseName,string connectionString)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
    }
}