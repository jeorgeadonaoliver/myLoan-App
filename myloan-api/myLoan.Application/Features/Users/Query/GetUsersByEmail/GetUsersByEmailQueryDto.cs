namespace myLoan.Application.Features.Users.Query.GetUsersByEmail;

public class GetUsersByEmailQueryDto
{
    public int UserId { get; set; }

    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;
}
