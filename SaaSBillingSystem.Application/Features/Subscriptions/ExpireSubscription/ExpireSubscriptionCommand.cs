using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.ExpireSubscription
{
    public class ExpireSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public ExpireSubscriptionCommand(Guid id)
        {
            Id = id;
        }
    }
}
