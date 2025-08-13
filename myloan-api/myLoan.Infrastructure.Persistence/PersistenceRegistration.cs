using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myLoan.Application.Interface.Common;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Infrastructure.Persistence.Models;
using myLoan.Infrastructure.Persistence.Repositories;
using myLoan.Infrastructure.Persistence.Validator;

namespace myLoan.Infrastructure.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceRegistration(this IServiceCollection service, IConfiguration config)
    {
        service.AddDbContext<MyLoanDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("myLoanDb")));

        // Fixing the syntax for AddScoped to properly register generic types
        service.AddScoped<IValidationHandler, ValidationHandlers>();
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        service.AddScoped<IUserRepository, UserRepository>();

        return service;
    }

}
