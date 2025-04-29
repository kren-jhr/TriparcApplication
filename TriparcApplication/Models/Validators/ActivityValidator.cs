using FluentValidation;
using TriparcApplication.Utils;

namespace TriparcApplication.Models;

public class ActivityValidator : AbstractValidator<Activity>
{
    public ActivityValidator()
    {
        // Convert to async validator if we need to lookup whether ActivityId is valid
        RuleFor(activity => activity.ActivityId)
            .NotEmpty()
            .WithMessage("ActivityId is required.")
            .Matches(RegexConstants.AlphaNumeric)
            .WithMessage("ActivityId may only contain letters, numbers, and underscores.");

        // Convert to async validator if we need to lookup whether DestinationId is valid
        RuleFor(activity => activity.DestinationId)
            .NotEmpty()
            .WithMessage("DestinationId is required.")
            .Matches(RegexConstants.AlphaNumeric)
            .WithMessage("ActivityId may only contain letters, numbers, and underscores.");

        RuleFor(activity => activity.Duration)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Duration must not be negative.");

        RuleFor(activity => activity.Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cost must not be negative.")
            .PrecisionScale(24, 2, false) // Arbitrary digits to left of decimal
            .WithMessage("Cost may only have two decimal places.");
    }
}