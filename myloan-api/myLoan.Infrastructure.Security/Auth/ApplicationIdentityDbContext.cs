using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myLoan.Domain.Entities;

namespace myLoan.Infrastructure.Security.Auth;

public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Always call base.OnModelCreating!

        // Seed Roles
        string adminRoleId = Guid.NewGuid().ToString();
        string userRoleId = Guid.NewGuid().ToString();

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            }
        );

        var hasher = new PasswordHasher<ApplicationUser>();
        string adminUserId = Guid.NewGuid().ToString();

        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@myloan.com",
                NormalizedUserName = "ADMIN@MYLOAN.COM",
                Email = "admin@myloan.com",
                NormalizedEmail = "ADMIN@MYLOAN.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "StrongP@ssw0rd"), 
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "System",
                LastName = "Admin",
                DateOfBirth = new DateTime(1990, 1, 1) 
            }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            }
        );
    }
}
