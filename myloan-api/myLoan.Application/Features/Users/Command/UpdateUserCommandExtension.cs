using myLoan.Domain.myLoanDbEntities;

namespace myLoan.Application.Features.Users.Command;

public static class UpdateUserCommandExtension
{
    public static User MapToEntity(this UpdateUserCommand cmd) 
    {
        return new User
        {
            UserId = cmd.UserId,
            AddressLine1 = cmd.AddressLine1,
            AddressLine2 = cmd.AddressLine2,
            CreatedAt = cmd.CreatedAt,
            UpdatedAt = cmd.UpdatedAt,
            City = cmd.City,
            Country = cmd.Country,
            DateOfBirth = cmd.DateOfBirth,
            Email  = cmd.Email,
            FirstName = cmd.FirstName,
            LastName = cmd.LastName,
            Phone = cmd.Phone,
            PostalCode = cmd.PostalCode,
            StateProvince = cmd.StateProvince,
            Status = cmd.Status,
        };
    }
}
