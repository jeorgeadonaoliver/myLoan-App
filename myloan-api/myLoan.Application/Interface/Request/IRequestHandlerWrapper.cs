namespace myLoan.Application.Interface.Request;

public interface IRequestHandlerWrapper
{
    Task<object> Handle(object request, CancellationToken cancellationToken);
}
