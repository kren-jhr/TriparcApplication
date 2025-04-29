namespace TriparcApplication.Models;

public class TripRequest
{
    public string UserId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Activity> Activities { get; set; } = new();

    // Convert TripRequest DTO to Trip domain model for use at service/repository layers
    public Trip ToTrip()
    {
        return new Trip
        {
            UserId = UserId,
            Title = Title,
            StartDate = StartDate,
            EndDate = EndDate,
            Activities = Activities
        };
    }
}