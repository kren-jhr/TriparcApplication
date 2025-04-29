using FluentValidation;

namespace TriparcApplication.Models;

public class ActivityValidator : AbstractValidator<Activity>
{
    public ActivityValidator()
    {
        RuleFor(activity => activity.ActivityId)
            .NotEmpty()
            .WithMessage("ActivityId is required.");

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