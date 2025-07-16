using FluentResults;
using myLoan.Application.Common;
using myLoan.Application.Interface.Auth;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Auth.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _repository;
    public RegisterCommandHandler(IAuthService authService, IUserRepository repository)
    {
        _authService = authService;
        _repository = repository;
    }

    public async Task<Result<string>> HandleAsync(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidationHandler.ValidateAsync(new RegisterCommandValidation(_authService), request, cancellationToken);
        if (validationResult.IsFailed)
            return Result.Fail(string.Join(";", validationResult.Errors));

        var result = await _authService.RegisterAsync(request.MapToApplicantUser());

        if (result.IsFailed)
            return Result.Fail(string.Join(";", validationResult.Errors));

        var userresult = await _repository.CreateAsync(request.MapToUser(), cancellationToken);
        if (userresult.IsFailed)
            return Result.Ok().WithError(string.Join(";", userresult.Errors));

        return Result.Ok(result.Value);
    }
}
