using Microsoft.EntityFrameworkCore;
using myLoan.Domain.myLoanDbEntities;

namespace myLoan.Infrastructure.Persistence.Models;

public partial class MyLoanDbContext : DbContext
{
    public MyLoanDbContext()
    {
    }

    public MyLoanDbContext(DbContextOptions<MyLoanDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<LoanRequest> LoanRequests { get; set; }

    public virtual DbSet<PaymentSchedule> PaymentSchedules { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loans__4F5AD4375A74C4D5");

            entity.HasIndex(e => e.RequestId, "UQ__Loans__33A8519B20F05200").IsUnique();

            entity.HasIndex(e => e.LoanNumber, "UQ__Loans__EEC266289EE5A592").IsUnique();

            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.AnnualInterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.LoanNumber).HasMaxLength(50);
            entity.Property(e => e.OutstandingBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Request).WithOne(p => p.Loan)
                .HasForeignKey<Loan>(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loans_LoanRequests");

            entity.HasOne(d => d.User).WithMany(p => p.Loans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loans_Users");
        });

        modelBuilder.Entity<LoanRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__LoanRequ__33A8519ADAB17AF3");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Purpose).HasMaxLength(500);
            entity.Property(e => e.RequestDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.RequestedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReviewedBy).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LoanRequests_Users");
        });

        modelBuilder.Entity<PaymentSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__PaymentS__9C8A5B69E7BD9917");

            entity.ToTable("PaymentSchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.InterestComponent).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalComponent).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Loan).WithMany(p => p.PaymentSchedules)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PaymentSchedule_Loans");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B13604C85");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.Loan).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Loans");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_Transactions_Schedule");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC86DC317F");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348A21A234").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AddressLine1).HasMaxLength(200);
            entity.Property(e => e.AddressLine2).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.StateProvince).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentType).HasMaxLength(200);
            entity.Property(e => e.UploadedAt).HasColumnType("datetime");
            entity.Property(e => e.Url).HasMaxLength(200);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
