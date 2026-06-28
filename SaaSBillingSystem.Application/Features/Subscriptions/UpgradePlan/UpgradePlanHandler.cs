using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.UpgradePlan
{
    public class UpgradePlanHandler: IRequestHandler<UpgradePlanCommand, Result>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPlanRepository _planRepository;
        public UpgradePlanHandler(ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
        }

        public async Task<Result> Handle(UpgradePlanCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result.Failure($"Subscription with id {command.Id} was not found");
            }

            var planExists = await _planRepository.ExistsAsync(command.NewPlanId, cancellationToken);
            if (!planExists)
            {
                return Result.Failure($"Plan with id {command.NewPlanId} was not found");
            }

            var result = subscription.UpgradePlan(command.NewPlanId);
            if (!result.IsSuccess)
            {
                return result;
            }
            await _subscriptionRepository.UpdateAsync(subscription);
            return Result.Success();
        }
    }
}
