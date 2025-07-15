using FluentResults;

namespace myLoan.Application.Common;

public static class PayloadHandler
{
    public static Payload<T> CreatePayload<T>(Result<T> result)
    {
        return new Payload<T>
        {
            success = result.IsSuccess,
            data = result.IsSuccess ? result.Value : default,
            message = result.IsSuccess ? $"{typeof(T)} success!" : string.Join("; ", result.Errors.Select(x => x.Message))
        };
    }
}
