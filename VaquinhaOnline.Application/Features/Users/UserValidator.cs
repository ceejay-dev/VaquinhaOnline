namespace VaquinhaOnline.Application.Features.Users;

public class UserValidator : AbstractValidator<UserCreateDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name is required.")
            .Length(2, 50).WithMessage("The name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The email is required.")
            .EmailAddress().WithMessage("The email must be a valid email address.");

        //RuleFor(x => x.PhoneNumber)
        //    .NotEmpty().WithMessage("The phone number is required.")
        //    .Matches(@"^\+?\d{10,15}$").WithMessage("The phone number must be valid (e.g., '+1234567890').");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password is required.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("The password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("The password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("The password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("The password must contain at least one special character.");
    }
}