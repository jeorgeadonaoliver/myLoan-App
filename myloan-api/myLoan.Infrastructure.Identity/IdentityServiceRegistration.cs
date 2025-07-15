using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using myLoan.Application.Interface.Auth;
using myLoan.Domain.Entities;
using myLoan.Infrastructure.Identity.Auth;

namespace myLoan.Infrastructure.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityService(this IServiceCollection service, IConfiguration config)
    {
        service.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("IdentityDb"),
            sql => sql.MigrationsAssembly("myLoan.Infrastructure.Identity")));

        service.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt => { 
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JwtSettings:Issuer"],
                ValidAudience = config["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(config["JwtSettings:Key"] ?? ""))
            };
        });

        service.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = true;
        })
       .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
       .AddDefaultTokenProviders();


        service.AddTransient<IAuthService, AuthService>();

        return service;
    }
}
