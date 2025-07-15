using myLoan.Domain.Entities;

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
    }
}
