namespace VaquinhaOnline.Application.Features.xs;

public class ProjectValidator : AbstractValidator<ProjectCreateDto>
{
    public ProjectValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The title is required.")
            .MaximumLength(100).WithMessage("The title must be at most 100 characters long.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The description is required.")
            .MaximumLength(1000).WithMessage("The description must be at most 1000 characters long.");

        RuleFor(x => x.Sector)
            .NotEmpty().WithMessage("The sector is required.")
            .MaximumLength(50).WithMessage("The sector must be at most 50 characters long.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("The status is required.")
            .Must(status => new[] { "Active", "Inactive", "Completed" }.Contains(status))
            .WithMessage("The status must be 'Active', 'Inactive', or 'Completed'.");

        RuleFor(x => x.GoalValue)
            .GreaterThan(0).WithMessage("The goal value must be greater than zero.");

        RuleFor(x => x.CurrentValue)
            .GreaterThanOrEqualTo(0).WithMessage("The current value must be greater than or equal to zero.")
            .LessThanOrEqualTo(x => x.GoalValue)
            .WithMessage("The current value cannot exceed the goal value.");

        RuleFor(x => x.PublicationDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The publication date cannot be in the future.");

        RuleFor(x => x.ClosingDate)
            .GreaterThan(x => x.PublicationDate)
            .WithMessage("The closing date must be after the publication date.");

        RuleFor(x => x.Progress)
            .NotEmpty().WithMessage("The progress is required.")
            .Must(x => new[] { "Not Funded", "Partially Funded", "Fully Funded", "Mentorship Phase" }
            .Contains(x))
            .WithMessage("The progress must be one of the following: 'Not Funded', 'Partially Funded', 'Fully Funded', or 'Mentorship Phase'.");

    }
}

