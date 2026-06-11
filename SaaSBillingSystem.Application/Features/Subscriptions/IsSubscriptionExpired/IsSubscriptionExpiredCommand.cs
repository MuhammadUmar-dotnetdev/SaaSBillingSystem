using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionExpired
{
    public class IsSubscriptionExpiredCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; private set; }

        public IsSubscriptionExpiredCommand(Guid id)
        {
            Id = id;
        }
    }
}
