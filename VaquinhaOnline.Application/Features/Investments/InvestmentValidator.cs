namespace VaquinhaOnline.Application.Features.Investments;

public class InvestmentValidator : AbstractValidator<InvestmentCreateDto>
{
    public InvestmentValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("The investment value must be greater than zero.");

        RuleFor(x => x.InvestmentDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("The investment date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("The investment date cannot be in the future.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("The description is required.")
            .MaximumLength(500)
            .WithMessage("The description must not exceed 500 characters.");

        RuleFor(x => x.InvestmentType)
            .NotEmpty()
            .NotNull()
            .WithMessage("The investment type is required.")
            .Must(type => new[] { "Monetary", "Mentorship", "Materials" }
                .Contains(type))
            .WithMessage("The investment type must be one of the following: Monetary, Mentorship, Materials.");
    }
}
