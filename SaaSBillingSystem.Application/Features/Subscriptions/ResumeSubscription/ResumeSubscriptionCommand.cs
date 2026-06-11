using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.ResumeSubscription
{
    public class ResumeSubscriptionCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }

        public ResumeSubscriptionCommand(Guid id)
        {
            Id = id;
        }
    }
}
