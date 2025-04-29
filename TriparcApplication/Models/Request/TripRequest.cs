using System.ComponentModel.DataAnnotations;

namespace TriparcApplication.Models;

public class TripRequest
{
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
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