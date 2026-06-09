using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.CreateSubscription
{
    public class CreateSubcriptionHandler : IRequestHandler<CreateSubscriptionCommand, Result<CreateSubscriptionResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public CreateSubcriptionHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<Result<CreateSubscriptionResponse>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            if(await _subscriptionRepository.ExistsAsync(command.OrganizationId, command.PlanId))
            {
                return Result<CreateSubscriptionResponse>.Failure("Subscription of this plan already exists for this organization");
            }

            var subscription = Subscription.Create(
                    command.OrganizationId,
                    command.PlanId,
                    command.StartDateUtc,
                    command.EndDateUtc,
                    command.IsTrial,
                    command.TrialEndAtUtc
                );

            await _subscriptionRepository.AddAsync(subscription);

            var response = new CreateSubscriptionResponse(subscription.OrganizationId, subscription.PlanId);

            return Result<CreateSubscriptionResponse>.Success(response);
        }
    }
}
