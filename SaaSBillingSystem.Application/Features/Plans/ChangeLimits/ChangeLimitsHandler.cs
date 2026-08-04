using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.ChangeLimits
{
    public class ChangeLimitsHandler: IRequestHandler<ChangeLimitsCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<ChangeLimitsHandler> _logger;
        public ChangeLimitsHandler(IPlanRepository planRepository, ILogger<ChangeLimitsHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(ChangeLimitsCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id, cancellationToken);
            if(plan == null)
            {
                _logger.LogWarning("Plan with id {Id} was not found", command.Id);
                return Result.Failure($"Plan with id {command.Id} was not found");
            }
            var result = plan.ChangeLimits(command.MaxUsers, command.MaxProjects, command.MaxStorageInMb);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("{Error} with id {Id}", result.Error, command.Id);
                return result;
            }
            await _planRepository.UpdateAsync(plan, cancellationToken);
            _logger.LogInformation("Plan with id {Id} successfully has its limits changed", command.Id);
            return Result.Success();
        }
    }
}
