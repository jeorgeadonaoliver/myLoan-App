using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myLoan.Domain.Entities;
using myLoan.Infrastructure.Identity.Auth;

namespace myLoan.RegisterUser.Api.ServiceExtention;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        dbContext.Database.Migrate();
    }
}

