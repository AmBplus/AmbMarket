
namespace _01_Framework.Domain.Execeptions;

public class InvalidMongoDbConnectionSettings : Exception
{
    public static string DefaultMessage
    {
        get => nameof(InvalidMongoDbConnectionSettings);
    }
    public InvalidMongoDbConnectionSettings() :base(DefaultMessage)
    {
        
    }
    public InvalidMongoDbConnectionSettings(string message) :base(message){}
    public InvalidMongoDbConnectionSettings(Exception innerException):base(DefaultMessage,innerException){}
    public InvalidMongoDbConnectionSettings(string message , Exception innerException):base(message,innerException){}
}