using FluentResults;
using FluentValidation;
using myLoan.Application.Common;
using myLoan.Application.Features.Users.Command;
using myLoan.Application.Features.Users.Query.GetUsers;
using myLoan.Application.Features.Users.Query.GetUsersByEmail;
using myLoan.Application.Features.Users.Query.GetUsersById;
using myLoan.Application.Interface.Common;
using myLoan.Application.Interface.Request;
using System.Reflection;

namespace myLoan.Api.Users.Extension
{
    public static class UsersServiceRegistration
    {
        public static IServiceCollection AddUsersService(this IServiceCollection service)
        {
            //service.AddValidatorsFromAssemblyContaining<UpdateUserCommandValidation>();
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            service.AddScoped<IRequestHandler<UpdateUserCommand, Result<int>>, UpdateUserCommandHandler>();
            service.AddScoped<IRequestHandler<GetUsersQuery, Result<IEnumerable<GetUsersQueryDto>>>, GetUsersQueryHandler>();
            service.AddScoped<IRequestHandler<GetUsersByEmailQuery, Result<IEnumerable<GetUsersByEmailQueryDto>>>, GetUsersByEmailQueryHandler>();
            service.AddScoped<IRequestHandler<GetUsersByIdQuery, Result<IEnumerable<GetUsersByIdQueryDto>>>, GetUsersByIdQueryHandler>();

            return service;
        }
    }
}
