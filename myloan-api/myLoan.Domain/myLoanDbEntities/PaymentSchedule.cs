using System;
using System.Collections.Generic;

namespace myLoan.Domain.myLoanDbEntities;

public partial class PaymentSchedule
{
    public int ScheduleId { get; set; }

    public int LoanId { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal PaymentAmount { get; set; }

    public decimal PrincipalComponent { get; set; }

    public decimal InterestComponent { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? PaidDate { get; set; }

    public virtual Loan Loan { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
