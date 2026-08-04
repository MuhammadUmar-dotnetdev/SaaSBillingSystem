using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlan;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPrivate
{
    public class MakePlanPrivateHandler: IRequestHandler<MakePlanPrivateCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        public readonly ILogger<MakePlanPrivateHandler> _logger;
        public MakePlanPrivateHandler(IPlanRepository planRepository, ILogger<MakePlanPrivateHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(MakePlanPrivateCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("Plan with id {Id} was not found", command.Id);
                return Result.Failure($"Plan with given id was not found");
            }
            var result = plan.MakePrivate();
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Plan with id {Id} is already set to private", command.Id);
                return result;
            }
            await _planRepository.UpdateAsync(plan, cancellationToken);
            _logger.LogInformation("Plan with id {Id} set to private", command.Id);
            return Result.Success();
        }
    }
}
