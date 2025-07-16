using System;
using System.Collections.Generic;

namespace myLoan.Domain.myLoanDbEntities;

public partial class LoanRequest
{
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public decimal RequestedAmount { get; set; }

    public byte RequestedTermMonths { get; set; }

    public string? Purpose { get; set; }

    public DateTime RequestDate { get; set; }

    public string Status { get; set; } = null!;

    public string? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? Notes { get; set; }

    public virtual Loan? Loan { get; set; }

    public virtual User User { get; set; } = null!;
}
