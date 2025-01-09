namespace VaquinhaOnline.Application.Features.Users;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty()
           .NotNull()
           .WithMessage("Email is required.")
           .EmailAddress()
           .WithMessage("Email must be valid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is required.");
    }
}
