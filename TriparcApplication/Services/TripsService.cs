using TriparcApplication.Models;
using TriparcApplication.Repositories;

namespace TriparcApplication.Services;

public class TripsService : ITripsService
{
    private readonly ITripsRepository _tripsRepository;

    public TripsService(ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }

    public async Task<List<Trip>> GetTripAsync() =>
        await _tripsRepository.GetAsync();

    public async Task<Trip?> GetTripAsync(string id) =>
        await _tripsRepository.GetAsync(id);

    public async Task<Trip> CreateTripAsync(TripRequest request)
    {
        try
        {
            var trip = request.ToTrip();
            trip.TotalCost = CalculateTripTotalCost(trip.Activities);

            await _tripsRepository.CreateAsync(trip);
            return trip;
        }
        catch (Exception e)
        {
            // Log exception
            throw new Exception("An error occurred while creating the trip.", e);
        }
        
    }

    // TotalCost is sum of all Activity costs
    private decimal CalculateTripTotalCost(List<Activity> activities) =>
        activities.Select(a => a.Cost).Sum();
}