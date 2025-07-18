using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using myLoan.Application.Features.Auth.Command.Login;
using myLoan.Application.Features.Auth.Command.Register;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Service;

public static class AuthenticationServiceRegistration
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection service) 
    {
        service.AddScoped<IRequestHandler<LoginCommand, Result<string>>,LoginCommandHandler>();
        service.AddScoped<IRequestHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();

        return service;
    }
}
