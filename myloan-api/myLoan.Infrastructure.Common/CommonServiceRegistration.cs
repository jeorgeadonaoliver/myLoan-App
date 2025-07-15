using Microsoft.Extensions.DependencyInjection;
using myLoan.Application.Interface.Request;
using myLoan.Infrastructure.Common.Request;

namespace myLoan.Infrastructure.Common;

public static class CommonServiceRegistration
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var handlerInterfaceType = typeof(IRequestHandler<,>);
        var wrapperType = typeof(RequestHandlerWrapper<,>);

        var handlerTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new
            {
                ServiceType = t.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType),
                ImplementationType = t
            })
            .Where(x => x.ServiceType != null);

        foreach (var type in handlerTypes)
        {
            var requestType = type.ServiceType.GenericTypeArguments[0];
            var responseType = type.ServiceType.GenericTypeArguments[1];

            var wrapperClosedType = wrapperType.MakeGenericType(requestType, responseType);

            services.AddTransient(type.ServiceType, type.ImplementationType);
            services.AddTransient(typeof(IRequestHandlerWrapper), wrapperClosedType);
        }

        services.AddScoped<IRequestDispatcher, RequestDispatcher>();
        services.AddScoped(typeof(RequestHandlerWrapper<,>));

        return services;
    }
}