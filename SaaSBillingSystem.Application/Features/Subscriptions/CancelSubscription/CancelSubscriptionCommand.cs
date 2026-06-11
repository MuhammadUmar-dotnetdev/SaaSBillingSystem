using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.CancelSubscription
{
    public class CancelSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public CancelSubscriptionCommand(Guid id)
        {
            Id = id;
        }
    }
}
