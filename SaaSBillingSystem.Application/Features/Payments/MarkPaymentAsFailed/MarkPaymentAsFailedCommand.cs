using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsFailed
{
    public class MarkPaymentAsFailedCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
        public string FailureReason { get; set; } = string.Empty;
    }
}
