using FluentResults;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Auth.Command.Register;

public class RegisterCommand : IRequest<Result<string>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
