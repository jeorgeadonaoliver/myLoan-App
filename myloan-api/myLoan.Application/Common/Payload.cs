namespace myLoan.Application.Common;

public class Payload<T>
{
    public bool success { get; set; }

    public T? data { get; set; }

    public string? message { get; set; } = string.Empty;
}
