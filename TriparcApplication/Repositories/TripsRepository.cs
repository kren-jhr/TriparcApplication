using TriparcApplication.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TriparcApplication.Configuration;

namespace TriparcApplication.Repositories;

public class TripsRepository
{
    private readonly IMongoCollection<Trip> tripCollection;

    public TripsRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(
            mongoDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDbSettings.Value.DatabaseName);

        tripCollection = mongoDatabase.GetCollection<Trip>(
            mongoDbSettings.Value.TripsCollectionName);
    }

    public async Task<List<Trip>> GetAsync() =>
        await tripCollection.Find(_ => true).ToListAsync();

    public async Task<Trip?> GetAsync(string id) =>
        await tripCollection.Find(x => x.TripId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Trip newBook) =>
        await tripCollection.InsertOneAsync(newBook);
}