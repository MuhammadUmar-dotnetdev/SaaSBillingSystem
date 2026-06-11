using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionActive
{
    public class IsSubscriptionActiveCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; private set; }

        public IsSubscriptionActiveCommand(Guid id)
        {
            Id = id;
        }
    }
}
