using FluentResults;
using FluentValidation;
using myLoan.Application.Features.Auth.Command.Login;
using myLoan.Application.Features.Auth.Command.Register;
using myLoan.Application.Interface.Request;
using System.Reflection;

namespace myLoan.Api.AuthenticateUser.ServiceExtention
{
    public static class AuthenticationServiceRegistration
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection service)
        {

            //service.AddValidatorsFromAssembly(typeof(LoginCommandValidation).Assembly);
            //service.AddValidatorsFromAssembly(typeof(RegisterCommandValidation).Assembly);

            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            service.AddScoped<IRequestHandler<LoginCommand, Result<string>>, LoginCommandHandler>();
            service.AddScoped<IRequestHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();

            return service;
        }
    }
}
