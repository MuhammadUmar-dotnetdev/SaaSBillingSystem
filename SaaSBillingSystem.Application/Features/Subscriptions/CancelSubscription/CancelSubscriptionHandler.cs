using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.CancelSubscription
{
    public class CancelSubscriptionHandler: IRequestHandler<CancelSubscriptionCommand, Result>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public CancelSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result> Handle(CancelSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result.Failure($"Subscription with id {command.Id} was not found");
            }

            var result = subscription.CancelImmediately();
            if (!result.IsSuccess)
            {
                return result;
            }
            await _subscriptionRepository.UpdateAsync(subscription);
            return Result.Success();
        }
    }
}
