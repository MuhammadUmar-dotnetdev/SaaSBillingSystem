namespace SaaSBillingSystem.Application.Features.Subscriptions.CreateSubscription
{
    public class CreateSubscriptionResponse
    {
        public Guid OrganizationId { get; private set; }
        public Guid PlanId { get; private set; }

        public CreateSubscriptionResponse(Guid organizationId, Guid planId)
        {
            OrganizationId = organizationId;
            PlanId = planId;
        }
    }
}
