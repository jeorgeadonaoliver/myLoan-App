using FluentResults;
using myLoan.Application.Common;
using myLoan.Application.Interface.Common;
using myLoan.Application.Interface.Auth;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Auth.Command.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IAuthService _authService;
    private readonly IValidationHandler _validationHandler;

    public LoginCommandHandler(IAuthService authService, IValidationHandler validationHandler)
    {
        _authService = authService;
        _validationHandler = validationHandler;
    }

    public async Task<Result<string>> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request is null) 
        {
            return Result.Fail<string>($"{request} cannot be null");
        }

        var validationResult = await _validationHandler.ValidateAsync<LoginCommand>(new LoginCommandValidation(), request, cancellationToken);
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
