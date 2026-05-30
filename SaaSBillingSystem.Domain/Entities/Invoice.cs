using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; }

    public Guid OrganizationId { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime IssuedAt { get; private set; }

    public DateTime DueDate { get; private set; }

    public bool IsPaid { get; private set; }

    private Invoice() { }

    public Invoice(Guid organizationId, decimal amount, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        OrganizationId = organizationId;
        Amount = amount;
        IssuedAt = DateTime.UtcNow;
        DueDate = dueDate;
        IsPaid = false;
    }

    public void MarkAsPaid()
    {
        if (IsPaid)
            throw new Exception("Invoice already paid.");

        IsPaid = true;
    }
}