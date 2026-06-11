using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.MarkPastDueSubscription
{
    public class MarkPastDueSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public MarkPastDueSubscriptionCommand(Guid id)
        {
            Id = id;
        }
    }
}
