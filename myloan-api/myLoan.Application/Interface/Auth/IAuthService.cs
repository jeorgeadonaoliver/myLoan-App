using FluentResults;
using myLoan.Domain.Entities;

namespace myLoan.Application.Interface.Auth;

public interface IAuthService
{
    Task<Result<(bool, ApplicationUser user)>> GetUserCredentials(string email);
    Task<Result<string>> RegisterAsync(ApplicationUser user);
    Task<Result<string>> LoginAsync(string email, string password);
}
