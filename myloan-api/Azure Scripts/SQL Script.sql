
CREATE TABLE dbo.Users (
    UserID          INT             IDENTITY(1,1)   PRIMARY KEY,
    FirstName       NVARCHAR(100)   NOT NULL,
    LastName        NVARCHAR(100)   NOT NULL,
    Email           NVARCHAR(255)   NOT NULL    UNIQUE,
    Phone           NVARCHAR(50)    NULL,
    DateOfBirth     DATE            NULL,
    AddressLine1    NVARCHAR(200)   NULL,
    AddressLine2    NVARCHAR(200)   NULL,
    City            NVARCHAR(100)   NULL,
    StateProvince   NVARCHAR(100)   NULL,
    PostalCode      NVARCHAR(20)    NULL,
    Country         NVARCHAR(100)   NULL,
    CreatedAt       DATETIME2       NOT NULL    DEFAULT SYSUTCDATETIME(),
    UpdatedAt       DATETIME2       NOT NULL    DEFAULT SYSUTCDATETIME(),
    Status          TINYINT         NOT NULL    DEFAULT 1       -- 1=Active, 0=Inactive
);
GO


-- 3.1 Capture each loan application before approval
CREATE TABLE dbo.LoanRequests (
    RequestID           INT             IDENTITY(1,1)   PRIMARY KEY,
    UserID              INT             NOT NULL,
    RequestedAmount     DECIMAL(18,2)   NOT NULL,
    RequestedTermMonths TINYINT         NOT NULL,            -- e.g., 12, 24, 36 months
    Purpose             NVARCHAR(500)   NULL,
    RequestDate         DATETIME2       NOT NULL    DEFAULT SYSUTCDATETIME(),
    Status              NVARCHAR(50)    NOT NULL    DEFAULT 'Pending',  
    ReviewedBy          NVARCHAR(100)   NULL,
    ReviewedAt          DATETIME2       NULL,
    Notes               NVARCHAR(MAX)   NULL,

    CONSTRAINT FK_LoanRequests_Users FOREIGN KEY(UserID)
        REFERENCES dbo.Users(UserID)
        ON DELETE CASCADE
);
GO-- 4.1 Once a request is approved, details move into active loans


CREATE TABLE dbo.Loans (
    LoanID              INT             IDENTITY(1,1)   PRIMARY KEY,
    RequestID           INT             NOT NULL    UNIQUE,   -- one-to-one with LoanRequests
    UserID              INT             NOT NULL,
    LoanNumber          NVARCHAR(50)    NOT NULL    UNIQUE,   -- e.g., ML-2025-000123
    PrincipalAmount     DECIMAL(18,2)   NOT NULL,
    AnnualInterestRate  DECIMAL(5,2)    NOT NULL,            -- e.g., 12.50 (%)
    TermMonths          TINYINT         NOT NULL,
    DisbursementDate    DATE            NULL,
    StartDate           DATE            NULL,
    MaturityDate        DATE            NULL,
    OutstandingBalance  DECIMAL(18,2)   NOT NULL,
    Status              NVARCHAR(50)    NOT NULL    DEFAULT 'Active',  
    CreatedAt           DATETIME2       NOT NULL    DEFAULT SYSUTCDATETIME(),
    UpdatedAt           DATETIME2       NOT NULL    DEFAULT SYSUTCDATETIME(),

    CONSTRAINT FK_Loans_LoanRequests FOREIGN KEY(RequestID)
        REFERENCES dbo.LoanRequests(RequestID),
    CONSTRAINT FK_Loans_Users FOREIGN KEY(UserID)
        REFERENCES dbo.Users(UserID)
);
GO

-- 5.1 Track each scheduled installment of a loan
CREATE TABLE dbo.PaymentSchedule (
    ScheduleID          INT             IDENTITY(1,1)   PRIMARY KEY,
    LoanID              INT             NOT NULL,
    DueDate             DATE            NOT NULL,
    PaymentAmount       DECIMAL(18,2)   NOT NULL,
    PrincipalComponent  DECIMAL(18,2)   NOT NULL,
    InterestComponent   DECIMAL(18,2)   NOT NULL,
    Status              NVARCHAR(20)    NOT NULL    DEFAULT 'Pending',  
    PaidDate            DATE            NULL,

    CONSTRAINT FK_PaymentSchedule_Loans FOREIGN KEY(LoanID)
        REFERENCES dbo.Loans(LoanID)
        ON DELETE CASCADE
);
GO

-- 6.1 Record all money movements (disbursements, repayments, fees)
CREATE TABLE dbo.Transactions (
    TransactionID       INT             IDENTITY(1,1)   PRIMARY KEY,
    LoanID              INT             NOT NULL,
    ScheduleID          INT             NULL,                 -- if tied to a specific installment
    TransactionType     NVARCHAR(50)    NOT NULL,             -- e.g., Disbursement, Repayment, LateFee
    Amount              DECIMAL(18,2)   NOT NULL,
    TransactionDate     DATETIME2       NOT NULL    DEFAULT SYSUTCDATETIME(),
    Description         NVARCHAR(500)   NULL,

    CONSTRAINT FK_Transactions_Loans FOREIGN KEY(LoanID)
        REFERENCES dbo.Loans(LoanID),
    CONSTRAINT FK_Transactions_Schedule FOREIGN KEY(ScheduleID)
        REFERENCES dbo.PaymentSchedule(ScheduleID)
);
GO





