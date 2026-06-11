using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.CancelSubscriptionAtEndOfPeriod
{
    public class CancelSubscriptionAtEndOfPeriodCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }

        public CancelSubscriptionAtEndOfPeriodCommand(Guid id)
        {
            Id = id;
        }
    }
}
