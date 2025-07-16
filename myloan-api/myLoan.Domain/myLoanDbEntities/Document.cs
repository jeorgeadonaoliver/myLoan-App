namespace myLoan.Domain.myLoanDbEntities;

public partial class Document
{
    public int Id { get; set; }

    public string? DocumentType { get; set; }

    public string? Url { get; set; }

    public DateTime? UploadedAt { get; set; }

    public int? UserId { get; set; }
}
