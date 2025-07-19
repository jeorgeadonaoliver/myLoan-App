using FluentResults;
using myLoan.Application.Extension;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Application.Interface.Request;

namespace myLoan.Application.Features.Users.Query.GetUsersByEmail;

public class GetUsersByEmailQueryHandler : IRequestHandler<GetUsersByEmailQuery, Result<IEnumerable<GetUsersByEmailQueryDto>>>
{
    private readonly IUserRepository _repository;

    public GetUsersByEmailQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetUsersByEmailQueryDto>>> HandleAsync(GetUsersByEmailQuery request, CancellationToken cancellationToken)
    {
        
        var result = await _repository.GetByAsync(x => x.Email == request.email, cancellationToken);
        if (result.IsFailed || !result.Value.Any()) 
        {
            return Result.Fail(result.Errors);
        }

        var mappedData = result.Value.Select(x => x.MapToGetUsersByEmailQueryDto());
        return Result.Ok(mappedData);
    }
}
