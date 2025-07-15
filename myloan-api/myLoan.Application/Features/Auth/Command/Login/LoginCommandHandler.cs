using FluentResults;
using myLoan.Application.Common;
using myLoan.Application.Interface.Auth;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Auth.Command.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<string>> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidationHandler.ValidateAsync(new LoginCommandValidation(), request, cancellationToken);
        if (validationResult.IsFailed) 
        {
            return Result.Fail<string>($"{string.Join(";", validationResult.Errors)}");
        }

        var result = await _authService.LoginAsync(request.Email, request.Password);

        if(result.IsFailed)
            return Result.Fail<string>($"{string.Join(";", result.Errors)}");

        return Result.Ok(result.Value);
    }
}
