using TriparcApplication.Models;

namespace TriparcApplication.Services;

public interface ITripsService
{
    Task<List<Trip>> GetTripAsync();
    Task<Trip?> GetTripAsync(string id);
    Task<Trip> CreateTripAsync(TripRequest tripRequest);
}