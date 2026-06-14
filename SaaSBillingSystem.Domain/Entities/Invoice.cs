using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; }

    public Guid OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;
    public Guid SubscriptionId { get; private set; }

    public Subscription Subscription { get; private set; } = null!;

    public decimal Amount { get; private set; }

    public DateTime IssuedAt { get; private set; }

    public DateTime DueDate { get; private set; }

    public InvoiceStatus Status { get; private set; }
    public DateTime? PaidAtUtc { get; private set; }
    public DateTime? CancelledAtUtc { get; private set; }
    public DateTime? OverdueAtUtc { get; private set; }
    public DateTime PeriodStartUtc { get; private set; }
    public DateTime PeriodEndUtc { get; private set; }
    public string InvoiceNumber { get; private set; } = string.Empty;

    private Invoice() { }

    public Invoice(
        Guid organizationId, 
        Guid subscriptionId, 
        decimal amount, 
        DateTime dueDate, 
        DateTime periodStartUtc, 
        DateTime periodEndUtc)
    {
        Id = Guid.NewGuid();
        OrganizationId = organizationId;
        SubscriptionId = subscriptionId;
        Amount = amount;
        IssuedAt = DateTime.UtcNow;
        DueDate = dueDate;
        Status = InvoiceStatus.Pending;
        PeriodStartUtc = periodStartUtc;
        PeriodEndUtc = periodEndUtc;
        InvoiceNumber = $"INV-{Id.ToString()[..8].ToUpper()}";
    }

    public static Result<Invoice> Create(
        Guid organizationId,
        Guid subscriptionId,
        decimal amount,
        DateTime dueDate,
        DateTime periodStartUtc,
        DateTime periodEndUtc)
    {
        if (amount <= 0)
        {
            return Result<Invoice>.Failure("Amount must be greater than zero.");
        }

        if (periodEndUtc <= periodStartUtc)
        {
            return Result<Invoice>.Failure("Period end must be after period start.");
        }

        var invoice = new Invoice(
            organizationId,
            subscriptionId,
            amount,
            dueDate,
            periodStartUtc,
            periodEndUtc);

        return Result<Invoice>.Success(invoice);
    }

    public Result MarkAsPaid()
    {
        if (Status == InvoiceStatus.Cancelled)
        {
            return Result.Failure("Cancelled invoice cannot be paid.");
        }

        if (Status == InvoiceStatus.Paid)
        {
            return Result.Failure("Invoice already paid.");
        }

        Status = InvoiceStatus.Paid;
        PaidAtUtc = DateTime.UtcNow;
        OverdueAtUtc = null;
        return Result.Success();
    }

    public Result MarkAsCancelled()
    {
        if (Status == InvoiceStatus.Paid)
        {
            return Result.Failure("Paid invoice cannot be cancelled.");
        }

        if (Status == InvoiceStatus.Cancelled)
        {
            return Result.Failure("Invoice is already cancelled");
        }
        Status = InvoiceStatus.Cancelled;
        CancelledAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result MarkAsOverdue()
    {
        if (Status == InvoiceStatus.Paid)
        {
            return Result.Failure("Paid invoice cannot become overdue.");
        }

        if (Status == InvoiceStatus.Cancelled)
        {
            return Result.Failure("Cancelled invoice cannot become overdue.");
        }

        if (Status == InvoiceStatus.Overdue)
        {
            return Result.Failure("Invoice is already overdue.");
        }

        Status = InvoiceStatus.Overdue;
        OverdueAtUtc = DateTime.UtcNow;
        return Result.Success();
    }
}