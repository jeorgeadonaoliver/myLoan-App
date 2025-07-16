using FluentResults;
using myLoan.Application.Extension;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<GetUsersQueryDto>>>
    {
        private readonly IUserRepository _repository;
        public GetUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GetUsersQueryDto>>> HandleAsync(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(cancellationToken);
            if (result.IsFailed) {
                return Result.Ok(Enumerable.Empty<GetUsersQueryDto>()).WithError(string.Join(";", result.Errors));
            }

            return Result.Ok(result.Value.Select(x => x.MapToGetUsersQueryDto()));
        }
    }
}
