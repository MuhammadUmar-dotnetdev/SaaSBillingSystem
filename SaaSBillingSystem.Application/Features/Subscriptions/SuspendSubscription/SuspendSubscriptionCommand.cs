using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.SuspendSubscription
{
    public class SuspendSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public SuspendSubscriptionCommand(Guid id)
        {
            Id = id;
        }
    }
}
