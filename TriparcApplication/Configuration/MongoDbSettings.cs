namespace TriparcApplication.Configuration;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TripsCollectionName { get; set; } = null!;
}