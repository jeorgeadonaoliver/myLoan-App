using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myLoan.Application.Interface.Request;

namespace myLoan.Infrastructure.Common.Request;

public class RequestDispatcher : IRequestDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RequestDispatcher> _logger;

    public RequestDispatcher(IServiceProvider serviceProvider, ILogger<RequestDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<Result<TResponse>> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var wrapperType = typeof(RequestHandlerWrapper<,>).MakeGenericType(requestType, typeof(TResponse));

        var wrapper = (IRequestHandlerWrapper)_serviceProvider.GetRequiredService(wrapperType);

        try
        {
            var response = (TResponse)await wrapper.Handle(request, cancellationToken);

            _logger.LogInformation($"Request of type {requestType.Name} handled successfully.");

            return response.ToResult().ValueOrDefault;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error handling request of type {requestType.Name}.");
            return Result.Fail<TResponse>(ex.Message);
        }
    }
}
