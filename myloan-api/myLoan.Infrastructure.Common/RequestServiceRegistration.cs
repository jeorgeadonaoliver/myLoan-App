using Microsoft.Extensions.DependencyInjection;
using myLoan.Application.Interface.Request;
using myLoan.Infrastructure.Common.Request;

namespace myLoan.Infrastructure.Common;

public static class RequestServiceRegistration
{
    public static IServiceCollection AddRequestService(this IServiceCollection service) 
    {
        service.AddScoped(typeof(RequestHandlerWrapper<,>));
        service.AddScoped<IRequestDispatcher, RequestDispatcher>();

        return service;
    }
}
