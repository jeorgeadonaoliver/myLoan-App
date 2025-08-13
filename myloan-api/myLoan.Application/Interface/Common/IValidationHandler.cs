using FluentResults;
using FluentValidation;

namespace myLoan.Application.Interface.Common
{
    public interface IValidationHandler
    {
        Task<Result> ValidateAsync<T>(IValidator<T> validator, T commandInstance, CancellationToken cancellationToken = default);
    }
}
