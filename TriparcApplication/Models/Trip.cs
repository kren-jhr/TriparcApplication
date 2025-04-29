using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TriparcApplication.Models;

public class Trip
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TripId { get; set; } = null!;
    public required string UserId { get; set; } = null!;
    public required string Title { get; set; } = null!;
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required List<Activity> Activities { get; set; }
    public decimal TotalCost { get; set; }
}