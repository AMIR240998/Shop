namespace ShopInfrastructure.Persistence.MongoDb;

public class MongoSetting
{
    public const string SectionName = "Mongo";

    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string Database { get; set; } = "ShopDb";
    public string ExceptionLogsCollection { get; set; } = "exception_Logs";

    public string CommentsCollection { get; set; } = "Comments";
}