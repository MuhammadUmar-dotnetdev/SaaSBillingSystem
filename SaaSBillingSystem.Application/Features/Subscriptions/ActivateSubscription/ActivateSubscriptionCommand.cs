using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.ActivateSubscription
{
    public class ActivateSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public ActivateSubscriptionCommand(Guid id)
        {
            Id = id;
        }
    }
}
