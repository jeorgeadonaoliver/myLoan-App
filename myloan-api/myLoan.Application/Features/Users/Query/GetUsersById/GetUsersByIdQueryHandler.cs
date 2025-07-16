using FluentResults;
using myLoan.Application.Extension;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Query.GetUsersById
{
    public class GetUsersByIdQueryHandler : IRequestHandler<GetUsersByIdQuery,Result<IEnumerable<GetUsersByIdQueryDto>>>
    {
        private readonly IUserRepository _repository;
        public GetUsersByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GetUsersByIdQueryDto>>> HandleAsync(GetUsersByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByAsync(x => x.UserId == request.id, cancellationToken);

            if (result.IsFailed) {
                return Result.Ok(Enumerable.Empty<GetUsersByIdQueryDto>()).WithError(string.Join(";", result.Errors));
            }

            return Result.Ok(result.Value.Select(x => x.MapToGetUsersByIdQueryDto()));
        }
    }
}
