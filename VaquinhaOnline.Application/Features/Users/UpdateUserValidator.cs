namespace VaquinhaOnline.Application.Features.Users;

public class UpdateUserValidator : AbstractValidator<UserUpdateDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name is required.")
            .Length(2, 50).WithMessage("The name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The email is required.")
            .EmailAddress().WithMessage("The email must be a valid email address.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Phone number is required.")
            .Matches(@"^\d{7,15}$")
            .WithMessage("Phone number must contain only digits and be between 7 and 15 characters long.");
    }
}
