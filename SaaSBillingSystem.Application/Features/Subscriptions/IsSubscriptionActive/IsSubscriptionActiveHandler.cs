using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionActive
{
    public class IsSubscriptionActiveHandler: IRequestHandler<IsSubscriptionActiveCommand, Result<bool>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public IsSubscriptionActiveHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result<bool>> Handle(IsSubscriptionActiveCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.Id);
            if (subscription == null)
            {
                return Result<bool>.Failure($"Subscription with id {command.Id} was not found");
            }
            return Result<bool>.Success(subscription.IsActive());
        }
    }
}
