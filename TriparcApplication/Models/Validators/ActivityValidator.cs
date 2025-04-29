using FluentValidation;

namespace TriparcApplication.Models;

public class ActivityValidator : AbstractValidator<Activity>
{
    public ActivityValidator()
    {
        // Convert to async validator if we need to lookup whether ActivityId is valid
        RuleFor(activity => activity.ActivityId)
            .NotEmpty()
            .WithMessage("ActivityId is required.");

        // Convert to async validator if we need to lookup whether DestinationId is valid
        RuleFor(activity => activity.DestinationId)
            .NotEmpty()
            .WithMessage("DestinationId is required.");

        RuleFor(activity => activity.Duration)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Duration must not be negative.");

        RuleFor(activity => activity.Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cost must not be negative.");
    }
}