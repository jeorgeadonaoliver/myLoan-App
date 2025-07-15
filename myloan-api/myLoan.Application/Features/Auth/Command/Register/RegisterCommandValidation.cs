using FluentValidation;
using myLoan.Application.Interface.Auth;

namespace myLoan.Application.Features.Auth.Command.Register;

public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
{
	private readonly IAuthService _authservice;
	public RegisterCommandValidation(IAuthService authService)
	{
		_authservice = authService;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Email not in proper format")
            .MustAsync(IsEmailExist).WithMessage("Account already Exist.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required!")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }

    private async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken) 
    {
        var result = await _authservice.GetUserCredentials(email);
        return !result.Value.Item1;
    }
}
