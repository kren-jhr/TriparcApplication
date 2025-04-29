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
    private readonly ITripsService _tripsService;
    private readonly IValidator<TripRequest> _tripRequestValidator;

    public TripsController(ITripsService tripsService, IValidator<TripRequest> tripRequestValidator)
    {
        _tripsService = tripsService;
        _tripRequestValidator = tripRequestValidator;
    }
        
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Get() 
    {
        try
        {
            var trips = await _tripsService.GetTripAsync();

            return Ok(trips);
        }
        catch (Exception e)
        {
            return Problem(
                title: "An error occurred while getting trips.",
                statusCode: (int)HttpStatusCode.InternalServerError,
                detail: e.Message
            );
        }   
    }
        

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var trip = await _tripsService.GetTripAsync(id);

            if (trip is null)
            {
                return NotFound();
            }

            return Ok(trip);
        }
        catch (Exception e)
        {
            return Problem(
                title: $"An error occurred while getting Trip {id}.",
                statusCode: (int)HttpStatusCode.InternalServerError,
                detail: e.Message
            );
        }   
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
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