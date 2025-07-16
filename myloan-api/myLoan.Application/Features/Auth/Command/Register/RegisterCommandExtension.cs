using myLoan.Domain.Entities;
using myLoan.Domain.myLoanDbEntities;

namespace myLoan.Application.Features.Auth.Command.Register
{
    public static class RegisterCommandExtension
    {
        public static ApplicationUser MapToApplicantUser(this RegisterCommand cmd) 
        {
            return new ApplicationUser { 
            
                LastName = cmd.LastName,
                FirstName = cmd.FirstName,
                Email = cmd.Email,
                PasswordHash = cmd.Password,
                DateOfBirth = cmd.DateOfBirth,
                UserName = cmd.Email,
            };
        }

        public static User MapToUser(this RegisterCommand cmd)
        {
            return new User
            {
                LastName = cmd.LastName,
                FirstName = cmd.FirstName,
                Email = cmd.Email,
                DateOfBirth = DateOnly.FromDateTime(cmd.DateOfBirth)
            };
        }
    }
}
