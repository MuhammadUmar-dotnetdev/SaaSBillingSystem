using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionInTrial
{
    public class IsSubscriptionInTrialHandler: IRequestHandler<IsSubscriptionInTrialCommand, Result<bool>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public IsSubscriptionInTrialHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result<bool>> Handle(IsSubscriptionInTrialCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result<bool>.Failure($"Subscription with id {command.Id} was not found");
            }
            return Result<bool>.Success(subscription.IsInTrial());
        }
    }
}
