INSERT INTO dbo.Users (
    FirstName, LastName, Email, Phone, DateOfBirth,
    AddressLine1, City, StateProvince, PostalCode, Country,
    CreatedAt, UpdatedAt, Status
) VALUES
('John', 'Doe', 'john.doe@example.com', '+1-555-123-4567', '1985-06-15',
 '123 Maple Street', 'Springfield', 'IL', '62701', 'USA',
 '2025-06-01 09:15:00', '2025-06-01 09:15:00', 1),
('Jane', 'Smith', 'jane.smith@example.com', '+1-555-234-5678', '1990-11-22',
 '456 Oak Avenue', 'Metropolis', 'NY', '10001', 'USA',
 '2025-06-02 10:30:00', '2025-06-02 10:30:00', 1);
GO


INSERT INTO dbo.LoanRequests (
    UserID, RequestedAmount, RequestedTermMonths, Purpose,
    RequestDate, Status, ReviewedBy, ReviewedAt, Notes
) VALUES
(1, 10000.00, 36, 'Home improvement',
 '2025-06-05 14:20:00', 'Approved', 'LoanOfficer1', '2025-06-07 10:00:00', 'Credit score 720'),
(2, 5000.00, 12, 'Car repair',
 '2025-07-01 09:45:00', 'Pending', NULL, NULL, NULL);
GO

INSERT INTO dbo.Loans (
    RequestID, UserID, LoanNumber, PrincipalAmount,
    AnnualInterestRate, TermMonths, DisbursementDate,
    StartDate, MaturityDate, OutstandingBalance,
    Status, CreatedAt, UpdatedAt
) VALUES
(1, 1, 'ML-2025-000001', 10000.00,
 5.50, 36, '2025-06-10',
 '2025-06-10', '2028-06-10', 10000.00,
 'Active', '2025-06-07 10:00:00', '2025-06-10 09:00:00');
GO


INSERT INTO dbo.PaymentSchedule (
    LoanID, DueDate, PaymentAmount,
    PrincipalComponent, InterestComponent,
    Status, PaidDate
) VALUES
(1, '2025-07-10', 301.54, 255.71,  45.83, 'Paid',    '2025-07-09'),
(1, '2025-08-10', 301.54, 256.89,  44.65, 'Pending', NULL),
(1, '2025-09-10', 301.54, 258.04,  43.50, 'Pending', NULL);
GO


INSERT INTO dbo.Transactions (
    LoanID, ScheduleID, TransactionType,
    Amount, TransactionDate, Description
) VALUES
(1, NULL, 'Disbursement',
 10000.00, '2025-06-10 09:00:00', 'Initial loan disbursement'),
(1, 1, 'Repayment',
 301.54, '2025-07-09 16:30:00', 'Monthly repayment – July 2025');
GO
