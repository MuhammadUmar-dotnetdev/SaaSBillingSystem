using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.MarkPastDueSubscription
{
    public class MarkPastDueSubscriptionHandler: IRequestHandler<MarkPastDueSubscriptionCommand, Result>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public MarkPastDueSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result> Handle(MarkPastDueSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result.Failure($"Subscription with id {command.Id} was not found");
            }

            var result = subscription.MarkPastDue();
            if (!result.IsSuccess)
            {
                return result;
            }
            await _subscriptionRepository.UpdateAsync(subscription);
            return Result.Success();
        }
    }
}
