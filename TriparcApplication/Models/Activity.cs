namespace TriparcApplication.Models;

public class Activity
{
    public required string ActivityId { get; set; } = null!;
    public required string DestinationId { get; set; } = null!;
    public int Duration { get; set; }
    public required decimal Cost { get; set; }
}