using Microsoft.AspNetCore.Mvc;
using TriparcApplication.Models;
using TriparcApplication.Services;

namespace TriparcApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly TripsService tripsService;

    public TripsController(TripsService tripsService) =>
        this.tripsService = tripsService;

    [HttpGet]
    public async Task<List<Trip>> Get() =>
        await tripsService.GetTripAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Trip>> Get(string id)
    {
        var trip = await tripsService.GetTripAsync(id);

        if (trip is null)
        {
            return NotFound();
        }

        return trip;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrip(TripRequest request)
    {
        var trip = await tripsService.CreateTripAsync(request);

        return CreatedAtAction(nameof(Get), new { id = trip.TripId }, trip);
    }
}