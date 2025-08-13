using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using myLoan.Application.Interface.Common;

namespace myLoan.Infrastructure.Persistence.Validator
{
    public class ValidationHandlers : IValidationHandler
    {
        public async Task<Result> ValidateAsync<T>(IValidator<T> validator, T commandInstance, CancellationToken cancellationToken = default)
        {
            if (commandInstance == null)
                return Result.Fail($"{typeof(T)} must not be null!");

            ValidationResult result = await validator.ValidateAsync(commandInstance, cancellationToken);
            if (result.IsValid)
                return Result.Ok();

            return Result.Fail(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
