using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.RenewSubscription
{
    public class RenewSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public DateTime NewEndDateUTC { get; private set; }

        public RenewSubscriptionCommand(Guid id, DateTime newEndDateUTC)
        {
            Id = id;
            NewEndDateUTC = newEndDateUTC;
        }
    }
}
