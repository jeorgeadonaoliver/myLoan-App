using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using myLoan.Application.Features.Users.Command;
using myLoan.Application.Features.Users.Query.GetUsers;
using myLoan.Application.Features.Users.Query.GetUsersByEmail;
using myLoan.Application.Features.Users.Query.GetUsersById;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Service;

public static class UsersServiceRegistration
{
    public static IServiceCollection AddUsersService(this IServiceCollection service) 
    {
        service.AddScoped<IRequestHandler<UpdateUserCommand, Result<int>>, UpdateUserCommandHandler>();
        service.AddScoped<IRequestHandler<GetUsersQuery, Result<IEnumerable<GetUsersQueryDto>>>, GetUsersQueryHandler>();
        service.AddScoped<IRequestHandler<GetUsersByEmailQuery, Result<IEnumerable<GetUsersByEmailQueryDto>>>, GetUsersByEmailQueryHandler>();
        service.AddScoped<IRequestHandler<GetUsersByIdQuery, Result<IEnumerable<GetUsersByIdQueryDto>>>, GetUsersByIdQueryHandler>();

        return service;
    }
}
