using FluentResults;

namespace myLoan.Application.Interface.Request;

public interface IRequestDispatcher
{
    Task<Result<TResponse>> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
