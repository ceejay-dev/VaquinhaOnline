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

        RuleFor(x => x.ClosingDate)
            .NotEmpty().NotNull()
            .WithMessage("The closing date required.");
    }
}

