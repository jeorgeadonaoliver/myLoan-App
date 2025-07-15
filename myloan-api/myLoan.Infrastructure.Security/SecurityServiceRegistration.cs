using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using myLoan.Domain.Entities;
using myLoan.Infrastructure.Security.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace myLoan.Infrastructure.Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("IdentityDb")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
       .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
       .AddDefaultTokenProviders();

        return services;
    }
}
