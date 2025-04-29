using System.Net;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TriparcApplication.Controllers;
using TriparcApplication.Models;
using TriparcApplication.Services;

namespace TriparcApplication.Tests.Controllers;

public class TripsControllerTests
{
    private readonly ITripsService _tripsService;

    public TripsControllerTests()
    {
        _tripsService = A.Fake<ITripsService>();
    }

    [Fact]
    public async Task TripsController_CreateTrip_Created()
    {
        #region Setup
        var tripRequest = new TripRequest
        {
            UserId = "User1",
            Title = "User1's France Trip",
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 10),
            Activities = new List<Activity>
            {
                new Activity
                {
                    ActivityId = "Activity1",
                    DestinationId = "Destination1",
                    Duration = 10000,
                    Cost = 100
                },
                new Activity
                {
                    ActivityId = "Activity2",
                    DestinationId = "Destination2",
                    Duration = 10000,
                    Cost = 500
                }
            }
        };

        var response = new Trip
        {
            UserId = tripRequest.UserId,
            Title = tripRequest.Title,
            StartDate = tripRequest.StartDate,
            EndDate = tripRequest.EndDate,
            Activities = tripRequest.Activities,
            TotalCost = 600
        };
        #endregion

        A.CallTo(() => _tripsService.CreateTripAsync(tripRequest))
            .Returns(Task.FromResult(response));

        var tripsController = new TripsController(_tripsService, new TripRequestValidator());
        var result = await tripsController.CreateTrip(tripRequest);
        var createdResult = result as CreatedAtActionResult;

        createdResult.Should().NotBeNull();
        Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
    }
    
    [Fact]
    public async Task TripsController_CreateTrip_BadRequest()
    {
        #region Setup
        var tripRequest = new TripRequest
        {
            UserId = "User1",
            Title = "User1's France Trip",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 1, 10),
            Activities = new List<Activity>
            {
                new Activity
                {
                    ActivityId = "Activity1",
                    DestinationId = "Destination1",
                    Duration = 10000,
                    Cost = 100
                },
                new Activity
                {
                    ActivityId = "Activity2",
                    DestinationId = "Destination2",
                    Duration = 10000,
                    Cost = 500
                }
            }
        };

        var response = new Trip
        {
            UserId = tripRequest.UserId,
            Title = tripRequest.Title,
            StartDate = tripRequest.StartDate,
            EndDate = tripRequest.EndDate,
            Activities = tripRequest.Activities,
            TotalCost = 600
        };
        #endregion

        A.CallTo(() => _tripsService.CreateTripAsync(tripRequest))
            .Returns(Task.FromResult(response));

        var tripsController = new TripsController(_tripsService, new TripRequestValidator());
        var result = await tripsController.CreateTrip(tripRequest);
        var badRequestResult = result as BadRequestObjectResult;

        badRequestResult.Should().NotBeNull();
        Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
    }
}