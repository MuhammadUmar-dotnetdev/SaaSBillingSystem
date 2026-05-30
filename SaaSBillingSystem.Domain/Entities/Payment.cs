namespace SaaSBillingSystem.Domain.Entities;

public class Payment
{
    public Guid Id { get; private set; }

    public Guid InvoiceId { get; private set; }

    public decimal Amount { get; private set; }

    public string Provider { get; private set; } = string.Empty;

    public string TransactionId { get; private set; } = string.Empty;

    public DateTime PaidAt { get; private set; }

    private Payment() { }

    public Payment(Guid invoiceId, decimal amount, string provider, string transactionId)
    {
        Id = Guid.NewGuid();
        InvoiceId = invoiceId;
        Amount = amount;
        Provider = provider;
        TransactionId = transactionId;
        PaidAt = DateTime.UtcNow;
    }
}