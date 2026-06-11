using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionInTrial
{
    public class IsSubscriptionInTrialCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; private set; }

        public IsSubscriptionInTrialCommand(Guid id)
        {
            Id = id;
        }
    }
}
