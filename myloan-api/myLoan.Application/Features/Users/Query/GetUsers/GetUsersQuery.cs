using FluentResults;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Query.GetUsers;

public record GetUsersQuery : IRequest<Result<IEnumerable<GetUsersQueryDto>>> { }
