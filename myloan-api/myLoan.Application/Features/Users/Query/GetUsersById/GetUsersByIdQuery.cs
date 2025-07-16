using FluentResults;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Query.GetUsersById;

public record GetUsersByIdQuery(int id) : IRequest<Result<IEnumerable<GetUsersByIdQueryDto>>>
{
}
