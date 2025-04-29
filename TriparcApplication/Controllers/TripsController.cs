using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TriparcApplication.Models;
using TriparcApplication.Services;

namespace TriparcApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly TripsService _tripsService;
    private readonly IValidator<TripRequest> _tripRequestValidator;

    public TripsController(TripsService tripsService, IValidator<TripRequest> tripRequestValidator)
    {
        _tripsService = tripsService;
        _tripRequestValidator = tripRequestValidator;
    }
        
    [HttpGet]
    public async Task<List<Trip>> Get() 
    {
        return await _tripsService.GetTripAsync();
    }
        

    [HttpGet("{id}")]
    public async Task<ActionResult<Trip>> Get(string id)
    {
        var trip = await _tripsService.GetTripAsync(id);

        if (trip is null)
        {
            return NotFound();
        }

        return trip;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrip(TripRequest request)
    {
        ValidationResult result = _tripRequestValidator.Validate(request);

        if(!result.IsValid)
        {
            return BadRequest(new { errors = result.Errors.Select(e => e.ErrorMessage) });
        }

        try
        {
            var trip = await _tripsService.CreateTripAsync(request);

            return CreatedAtAction(nameof(Get), new { id = trip.TripId }, trip);
        }
        catch (Exception e)
        {
            return Problem(
                title: "An error occurred while attempting to create the trip.",
                statusCode: (int)HttpStatusCode.InternalServerError,
                detail: e.Message
            );
        }        
    }
}