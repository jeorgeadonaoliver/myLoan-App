using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Infrastructure.Persistence.Models;
using myLoan.Infrastructure.Persistence.Repositories;

namespace myLoan.Infrastructure.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<MyLoanDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("myLoanDb")));

        // Fixing the syntax for AddScoped to properly register generic types
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

}
