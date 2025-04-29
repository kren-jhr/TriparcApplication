using TriparcApplication.Models;

namespace TriparcApplication.Repositories;

public interface ITripsRepository
{
    Task<List<Trip>> GetAsync();
    Task<Trip?> GetAsync(string id);
    Task CreateAsync(Trip trip);
}