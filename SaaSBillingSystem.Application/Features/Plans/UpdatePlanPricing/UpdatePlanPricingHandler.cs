using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.UpdatePlanPricing
{
    public class UpdatePlanPricingHandler: IRequestHandler<UpdatePlanPricingCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<UpdatePlanPricingHandler> _logger;
        public UpdatePlanPricingHandler(IPlanRepository planRepository, ILogger<UpdatePlanPricingHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePlanPricingCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("Plan with id {Id} was not found", command.Id);
                return Result.Failure($"Plan with given id was not found");
            }
            var result = plan.UpdatePricing(command.Price);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("{Error} with id {Id}", result.Error, command.Id);
                return result;
            }
            await _planRepository.UpdateAsync(plan, cancellationToken);
            _logger.LogInformation("Plan with id {Id} has successfully updated pricing", command.Id);
            return Result.Success();
        }
    }
}
