using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlan;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.DeactivatePlan
{
    public class DeactivatePlanHandler: IRequestHandler<DeactivatePlanCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<DeactivatePlanHandler> _logger;
        public DeactivatePlanHandler(IPlanRepository planRepository, ILogger<DeactivatePlanHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }
        public async Task<Result> Handle(DeactivatePlanCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("Plan with id {Id} was not found", command.Id);
                return Result.Failure($"Plan with given id was not found");
            }
            var result = plan.Deactivate();
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Plan with id {Id} is already deactivated", command.Id);
                return result;
            }
            await _planRepository.UpdateAsync(plan, cancellationToken);
            _logger.LogInformation("Plan with id {Id} is deactivated successfully", command.Id);
            return Result.Success();
        }
    }
}
