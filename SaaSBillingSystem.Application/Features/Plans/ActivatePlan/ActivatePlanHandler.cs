using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlan;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.ActivatePlan
{
    public class ActivatePlanHandler: IRequestHandler<ActivatePlanCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<ActivatePlanHandler> _logger;
        public ActivatePlanHandler(IPlanRepository planRepository, ILogger<ActivatePlanHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }
        public async Task<Result> Handle(ActivatePlanCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("Plan with id {Id} was not found", command.Id);
                return Result.Failure($"Plan with given id was not found");
            }
            var result = plan.Activate();
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Plan with id {Id} is already activated", command.Id);
                return result;
            }
            await _planRepository.UpdateAsync(plan, cancellationToken);
            _logger.LogInformation("Plan with id {Id} is activated successfully", command.Id);
            return Result.Success();
        }
    }
}
