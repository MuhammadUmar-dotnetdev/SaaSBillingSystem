using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

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

    public Result Activate()
    {
        if (Status == SubscriptionStatus.Active)
            return Result.Failure("Subscription is already active.");

        Status = SubscriptionStatus.Active;
        IsTrial = false;
        return Result.Success();
    }

    public Result MarkPastDue()
    {
        if(Status == SubscriptionStatus.PastDue)
        {
            return Result.Failure("Subscription is already set to past due.");
        }
        Status = SubscriptionStatus.PastDue;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Suspend()
    {
        if(Status == SubscriptionStatus.Suspended)
        {
            return Result.Failure("Subscription is already suspended");
        }
        Status = SubscriptionStatus.Suspended;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Expire()
    {
        if(Status == SubscriptionStatus.Expired)
        {
            return Result.Failure("Subscription is already expired");
        }
        Status = SubscriptionStatus.Expired;
        AutoRenew = false;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result CancelImmediately()
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            return Result.Failure("Subscription is already expired");
        }
        Status = SubscriptionStatus.Cancelled;
        CancelledAtUtc = DateTime.UtcNow;
        AutoRenew = false;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result CancelAtEndOfPeriod()
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            return Result.Failure("Subscription is already cancelled.");
        }

        if (Status == SubscriptionStatus.Expired)
        {
            return Result.Failure("Subscription is already expired.");
        }

        if (CancelAtPeriodEnd)
        {
            return Result.Failure("Subscription is already scheduled for cancellation.");
        }

        CancelAtPeriodEnd = true;
        AutoRenew = false;
        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result Resume()
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            return Result.Failure("Cannot resume a cancelled subscription.");
        }

        if (Status == SubscriptionStatus.Expired)
        {
            return Result.Failure("Cannot resume an expired subscription.");
        }

        if (!CancelAtPeriodEnd)
        {
            return Result.Failure("Subscription is not scheduled for cancellation.");
        }

        CancelAtPeriodEnd = false;
        AutoRenew = true;
        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result Renew(DateTime newEndDateUtc)
    {
        if(EndDateUtc == newEndDateUtc)
        {
            return Result.Failure("This plan already have end date set to this");
        }
        EndDateUtc = newEndDateUtc;
        Status = SubscriptionStatus.Active;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result UpgradePlan(Guid newPlanId)
    {
        if(PlanId == newPlanId)
        {
            return Result.Failure("This subscription already have this plan");
        }
        PlanId = newPlanId;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result DowngradePlan(Guid newPlanId)
    {
        if(PlanId == newPlanId)
        {
            return Result.Failure("This subscription already belongs to this plan");
        }
        PlanId = newPlanId;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
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