namespace VaquinhaOnline.Application.Features.Investments;

public class InvestmentValidator : AbstractValidator<InvestmentCreateDto>
{
    public InvestmentValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("The investment value must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("The description is required.")
            .MaximumLength(500)
            .WithMessage("The description must not exceed 500 characters.");

        RuleFor(x => x.InvestmentType)
            .NotEmpty()
            .NotNull()
            .WithMessage("The investment type is required.")
            .Must(type => new[] { "Financeiro", "Mentoria", "Materiais" }
                .Contains(type))
            .WithMessage("The investment type must be one of the following: Financeiro, Mentoria, Materiais.");
    }
}
