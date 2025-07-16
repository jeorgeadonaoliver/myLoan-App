using FluentResults;
using myLoan.Application.Common;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Command;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
{
    private readonly IUserRepository _repositopry;
    public UpdateUserCommandHandler(IUserRepository repository)
    {
        _repositopry = repository;
    }
    public async Task<Result<int>> HandleAsync(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidationHandler.ValidateAsync(new UpdateUserCommandValidation(_repositopry), request, cancellationToken);
        if(validationResult.IsFailed)
            return Result.Fail(string.Join(";", validationResult.Errors));

        var result = await _repositopry.UpdateAsync(request.MapToEntity(), cancellationToken);
        if (result.IsFailed || result.Value == 0)
            return Result.Fail(string.Join(";", result.Errors));

        return result.Value;
    }
}
