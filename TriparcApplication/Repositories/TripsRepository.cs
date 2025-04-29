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

    public async Task CreateAsync(Trip trip) 
    {
        try
        {
            await tripCollection.InsertOneAsync(trip);
        }
        catch (MongoWriteException e)
        {
            // Write to log or monitoring provider
            Console.WriteLine(trip.ToString());
            throw new Exception($"Could not write Trip {trip.TripId} to database", e);
        }
        catch (Exception e)
        {
            // Generic error handling
            // Write to log or monitoring provider
            Console.WriteLine(trip.ToString());
            throw new Exception($"Error occurred while attempting to create Trip {trip.TripId}", e);
        }
        
    }
}