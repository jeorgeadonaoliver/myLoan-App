using FluentValidation;
using myLoan.Application.Interface.MyLoanRepository;

namespace myLoan.Application.Features.Users.Command;

public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _repository;
    public UpdateUserCommandValidation(IUserRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Must be valid Email Address format!");

        RuleFor(x => x)
            .MustAsync(isEmailExist).WithMessage("Email aleardy exist!");

    }

    private async Task<bool> isEmailExist(UpdateUserCommand cmd, CancellationToken cancellationToken) {
        var result = await _repository.GetAnyAsync(x => x.Email == cmd.Email && x.UserId == cmd.UserId, cancellationToken);
        return result.IsSuccess ? result.Value : !result.Value;
    }
}
