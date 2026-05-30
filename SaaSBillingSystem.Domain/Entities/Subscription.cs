using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Domain.Entities;

public class Subscription
{
    public Guid Id { get; private set; }

    public Guid OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public Guid PlanId { get; private set; }
    public Plan Plan { get; private set; } = null!;

    public SubscriptionStatus Status { get; private set; }

    public DateTime StartDateUtc { get; private set; }
    public DateTime EndDateUtc { get; private set; }

    public bool IsTrial { get; private set; }
    public DateTime? TrialEndsAtUtc { get; private set; }

    public bool CancelAtPeriodEnd { get; private set; }
    public DateTime? CancelledAtUtc { get; private set; }

    public bool AutoRenew { get; private set; }

    public string? ExternalSubscriptionId { get; private set; }
    public string? ExternalCustomerId { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    private Subscription() { }

    public static Subscription Create(
        Guid organizationId,
        Guid planId,
        DateTime startDateUtc,
        DateTime endDateUtc,
        bool isTrial = false,
        DateTime? trialEndsAtUtc = null)
    {
        return new Subscription
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            PlanId = planId,
            Status = isTrial
                ? SubscriptionStatus.Trialing
                : SubscriptionStatus.Active,

            StartDateUtc = startDateUtc,
            EndDateUtc = endDateUtc,

            IsTrial = isTrial,
            TrialEndsAtUtc = trialEndsAtUtc,

            AutoRenew = true,

            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };
    }

    public void Activate()
    {
        Status = SubscriptionStatus.Active;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkPastDue()
    {
        Status = SubscriptionStatus.PastDue;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Suspend()
    {
        Status = SubscriptionStatus.Suspended;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Expire()
    {
        Status = SubscriptionStatus.Expired;
        AutoRenew = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void CancelImmediately()
    {
        Status = SubscriptionStatus.Cancelled;
        CancelledAtUtc = DateTime.UtcNow;
        AutoRenew = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void CancelAtEndOfPeriod()
    {
        CancelAtPeriodEnd = true;
        AutoRenew = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Resume()
    {
        if (Status != SubscriptionStatus.Cancelled &&
            Status != SubscriptionStatus.Expired)
        {
            CancelAtPeriodEnd = false;
            AutoRenew = true;
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }

    public void Renew(DateTime newEndDateUtc)
    {
        EndDateUtc = newEndDateUtc;
        Status = SubscriptionStatus.Active;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void UpgradePlan(Guid newPlanId)
    {
        PlanId = newPlanId;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void DowngradePlan(Guid newPlanId)
    {
        PlanId = newPlanId;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public bool IsActive()
    {
        return Status == SubscriptionStatus.Active
            || Status == SubscriptionStatus.Trialing;
    }

    public bool IsExpired()
    {
        return EndDateUtc <= DateTime.UtcNow;
    }

    public bool IsInTrial()
    {
        return IsTrial
            && TrialEndsAtUtc.HasValue
            && TrialEndsAtUtc.Value > DateTime.UtcNow;
    }

    public void SetExternalProviderData(
        string externalSubscriptionId,
        string externalCustomerId)
    {
        ExternalSubscriptionId = externalSubscriptionId;
        ExternalCustomerId = externalCustomerId;

        UpdatedAtUtc = DateTime.UtcNow;
    }
}