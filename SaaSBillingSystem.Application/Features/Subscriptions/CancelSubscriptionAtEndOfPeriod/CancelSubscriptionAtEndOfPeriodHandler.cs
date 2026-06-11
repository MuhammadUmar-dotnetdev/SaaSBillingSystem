using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.CancelSubscriptionAtEndOfPeriod
{
    public class CancelSubscriptionAtEndOfPeriodHandler: IRequestHandler<CancelSubscriptionAtEndOfPeriodCommand, Result>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public CancelSubscriptionAtEndOfPeriodHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result> Handle(CancelSubscriptionAtEndOfPeriodCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result.Failure($"Subscription with id {command.Id} was not found");
            }
            var result = subscription.CancelAtEndOfPeriod();
            if (!result.IsSuccess)
            {
                return result;
            }
            await _subscriptionRepository.UpdateAsync(subscription);
            return Result.Success();
        }
    }
}
