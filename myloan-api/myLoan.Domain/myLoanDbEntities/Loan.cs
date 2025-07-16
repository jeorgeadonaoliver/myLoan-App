using System;
using System.Collections.Generic;

namespace myLoan.Domain.myLoanDbEntities;

public partial class Loan
{
    public int LoanId { get; set; }

    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string LoanNumber { get; set; } = null!;

    public decimal PrincipalAmount { get; set; }

    public decimal AnnualInterestRate { get; set; }

    public byte TermMonths { get; set; }

    public DateOnly? DisbursementDate { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? MaturityDate { get; set; }

    public decimal OutstandingBalance { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();

    public virtual LoanRequest Request { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
