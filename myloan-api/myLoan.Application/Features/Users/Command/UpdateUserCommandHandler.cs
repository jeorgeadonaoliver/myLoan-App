using FluentResults;
using myLoan.Application.Common;
using myLoan.Application.Interface.Common;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Command;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
{
    private readonly IUserRepository _repository;
    private readonly IValidationHandler _validationHandler;

    public UpdateUserCommandHandler(IUserRepository repository, IValidationHandler validationHandler)
    {
        _repository = repository;
        _validationHandler = validationHandler;
    }

    public async Task<Result<int>> HandleAsync(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validationHandler.ValidateAsync(new UpdateUserCommandValidation(_repository), request, cancellationToken);
        if(validationResult.IsFailed)
            return Result.Fail(ErrorHandler.AgggateErrors(validationResult.Errors));

        var result = await _repository.UpdateAsync(request.MapToEntity(), cancellationToken);
        if (result.IsFailed || result.Value == 0)
            return Result.Fail(string.Join(";", result.Errors));

        return Result.Ok(result.Value);
    }
}
