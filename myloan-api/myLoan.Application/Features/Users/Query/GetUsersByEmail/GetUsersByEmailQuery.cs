using FluentResults;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Query.GetUsersByEmail;

public record GetUsersByEmailQuery(string email) : IRequest<Result<IEnumerable<GetUsersByEmailQueryDto>>> { }
