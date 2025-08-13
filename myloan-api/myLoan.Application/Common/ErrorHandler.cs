using FluentResults;

namespace myLoan.Application.Common
{
    public static class ErrorHandler
    {
        public static string AgggateErrors(IReadOnlyList<FluentResults.IError> errors)
        {
            return string.Join("; ", errors);
        }
    }
}
