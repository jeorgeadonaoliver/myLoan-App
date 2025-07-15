using FluentValidation;

namespace myLoan.Application.Features.Auth.Command.Login;

public class LoginCommandValidation : AbstractValidator<LoginCommand>
{
    public LoginCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
