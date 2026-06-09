using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.CreateSubscription
{
    public class CreateSubscriptionCommand: IRequest<Result<CreateSubscriptionResponse>>
    {
        public Guid OrganizationId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
        public bool IsTrial { get; set; }
        public DateTime TrialEndAtUtc { get; set; }
    }
}
