using FluentValidation;

namespace TriparcApplication.Models;

public class TripRequestValidator : AbstractValidator<TripRequest>
{
    public TripRequestValidator()
    {
        // If we need to lookup whether UserId is valid, convert to async validator
        RuleFor(request => request.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(request => request.Title)
            .NotEmpty()
            .WithMessage("Title is required.");

        RuleFor(request => request.StartDate)
            .NotEmpty()
            .WithMessage("StartDate is required.")
            .LessThan(request => request.EndDate)
            .WithMessage("StartDate must be before EndDate.");

        RuleFor(request => request.EndDate)
            .NotEmpty()
            .WithMessage("EndDate is required.");

        RuleFor(request => request.Activities)
            .NotNull()
            .WithMessage("Activities is required.");

        RuleForEach(request => request.Activities)
        .SetValidator(new ActivityValidator());
    }
}