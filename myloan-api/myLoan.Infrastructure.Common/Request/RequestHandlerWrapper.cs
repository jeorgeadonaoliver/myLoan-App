using myLoan.Application.Interface.Request;

namespace myLoan.Infrastructure.Common.Request;

public class RequestHandlerWrapper<TRequest, TResponse> : IRequestHandlerWrapper where TRequest : IRequest<TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> _handler;

    public RequestHandlerWrapper(IRequestHandler<TRequest, TResponse> handler)
    {
        _handler = handler;
    }

    public async Task<object> Handle(object request, CancellationToken cancellationToken)
    {
        return await _handler.HandleAsync((TRequest)request, cancellationToken);
    }
}
