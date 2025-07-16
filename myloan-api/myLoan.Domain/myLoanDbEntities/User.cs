namespace myLoan.Domain.myLoanDbEntities;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? StateProvince { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public byte Status { get; set; }

    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
