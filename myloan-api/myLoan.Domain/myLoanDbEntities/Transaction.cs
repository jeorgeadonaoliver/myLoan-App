using System;
using System.Collections.Generic;

namespace myLoan.Domain.myLoanDbEntities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int LoanId { get; set; }

    public int? ScheduleId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Description { get; set; }

    public virtual Loan Loan { get; set; } = null!;

    public virtual PaymentSchedule? Schedule { get; set; }
}
