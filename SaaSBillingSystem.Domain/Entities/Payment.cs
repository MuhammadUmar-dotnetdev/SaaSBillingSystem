using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Domain.Entities;

public class Payment
{
    public Guid Id { get; private set; }

    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;

    public decimal Amount { get; private set; }

    public string Provider { get; private set; } = string.Empty;

    public DateTime? PaidAt { get; private set; }
    public DateTime? RefundedAt { get; private set; }
    public PaymentStatus Status { get; private set; }

    public string? ExternalPaymentId { get; private set; }

    public PaymentCurrency Currency { get; private set; } = PaymentCurrency.USD;
    public string? FailureReason { get; private set; }
    private Payment() { }

    private Payment(Guid invoiceId, PaymentCurrency currency, decimal amount, string provider, string externalPaymentId)
    {
        Id = Guid.NewGuid();
        Currency = currency;
        InvoiceId = invoiceId;
        Amount = amount;
        Provider = provider;
        Status = PaymentStatus.Pending;
        ExternalPaymentId = externalPaymentId;
    }

    public static Result<Payment> Create(Guid invoiceId, PaymentCurrency currency, decimal amount, string provider, string externalPaymentId)
    {
        if (amount <= 0)
        {
            return Result<Payment>.Failure("Amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(provider))
        {
            return Result<Payment>.Failure("Provider is required.");
        }

        var payment = new Payment(invoiceId, currency, amount, provider, externalPaymentId);
        return Result<Payment>.Success(payment);
    }

    public Result MarkAsPaid()
    {
        if (Status == PaymentStatus.Succeeded)
        {
            return Result.Failure("Payment is already succeeded");
        }

        if (Status == PaymentStatus.Failed)
        {
            return Result.Failure("Failed payment cannot be paid.");
        }
        PaidAt = DateTime.UtcNow;
        FailureReason = null;
        Status = PaymentStatus.Succeeded;
        return Result.Success();
    }

    public Result MarkAsFailed(string failureReason)
    {
        if (Status == PaymentStatus.Succeeded)
        {
            return Result.Failure("Succeeded payment cannot fail");
        }
        FailureReason = failureReason;
        Status = PaymentStatus.Failed;
        PaidAt = null;
        return Result.Success();
    }

    public Result MarkAsRefund()
    {
        if (Status == PaymentStatus.Pending)
        {
            return Result.Failure("Cannot refund pending payment");
        }

        if (Status == PaymentStatus.Failed)
        {
            return Result.Failure("Cannot refund failed payment");
        }

        if (Status == PaymentStatus.Refunded)
        {
            return Result.Failure("This payment is already refunded");
        }

        Status = PaymentStatus.Refunded;
        RefundedAt = DateTime.UtcNow;
        PaidAt = null;
        return Result.Success();
    }
}