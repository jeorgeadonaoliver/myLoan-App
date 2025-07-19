using myLoan.Application.Features.Users.Query.GetUsers;
using myLoan.Application.Features.Users.Query.GetUsersByEmail;
using myLoan.Application.Features.Users.Query.GetUsersById;
using myLoan.Domain.myLoanDbEntities;

namespace myLoan.Application.Extension;

public static class UsersExtension
{
    public static GetUsersQueryDto MapToGetUsersQueryDto(this User users) 
    {
        return new GetUsersQueryDto { 
        
            AddressLine1 = users.AddressLine1,
            AddressLine2 = users.AddressLine2,
            CreatedAt = users.CreatedAt,
            UpdatedAt = users.UpdatedAt,
            City = users.City,
            Country = users.Country,
            DateOfBirth = users.DateOfBirth,
            Email = users.Email,
            FirstName = users.FirstName,
            LastName = users.LastName,
            Phone = users.Phone,
            PostalCode = users.PostalCode,
            StateProvince = users.StateProvince,
            Status = users.Status,
            UserId = users.UserId,

        };
    }

    public static GetUsersByIdQueryDto MapToGetUsersByIdQueryDto(this User users)
    {
        return new GetUsersByIdQueryDto
        {

            AddressLine1 = users.AddressLine1,
            AddressLine2 = users.AddressLine2,
            CreatedAt = users.CreatedAt,
            UpdatedAt = users.UpdatedAt,
            City = users.City,
            Country = users.Country,
            DateOfBirth = users.DateOfBirth,
            Email = users.Email,
            FirstName = users.FirstName,
            LastName = users.LastName,
            Phone = users.Phone,
            PostalCode = users.PostalCode,
            StateProvince = users.StateProvince,
            Status = users.Status,
            UserId = users.UserId,

        };
    }

    public static GetUsersByEmailQueryDto MapToGetUsersByEmailQueryDto(this User users) 
    {
        return new GetUsersByEmailQueryDto 
        {
            AddressLine1 = users.AddressLine1,
            AddressLine2 = users.AddressLine2,
            CreatedAt = users.CreatedAt,
            UpdatedAt = users.UpdatedAt,
            City = users.City,
            Country = users.Country,
            DateOfBirth = users.DateOfBirth,
            Email = users.Email,
            FirstName = users.FirstName,
            LastName = users.LastName,
            Phone = users.Phone,
            PostalCode = users.PostalCode,
            StateProvince = users.StateProvince,
            Status = users.Status,
            UserId = users.UserId,

        };
    }
}
