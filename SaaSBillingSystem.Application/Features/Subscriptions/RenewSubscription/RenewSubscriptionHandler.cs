using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.RenewSubscription
{
    public class RenewSubscriptionHandler: IRequestHandler<RenewSubscriptionCommand, Result>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public RenewSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result> Handle(RenewSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result.Failure($"Subscription with id {command.Id} was not found");
            }

            var result = subscription.Renew(command.NewEndDateUTC);
            if (!result.IsSuccess)
            {
                return result;
            }
            await _subscriptionRepository.UpdateAsync(subscription);
            return Result.Success();
        }
    }
}
