using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPublic
{
    public class MakePlanPublicHandler: IRequestHandler<MakePlanPublicCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<MakePlanPublicHandler> _logger;
        public MakePlanPublicHandler(IPlanRepository planRepository, ILogger<MakePlanPublicHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(MakePlanPublicCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("Plan with id {Id} was not found", command.Id);
                return Result.Failure($"Plan with given id was not found");
            }
            var result = plan.MakePublic();
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Plan with id {Id} is already set to public", command.Id);
                return result;
            }
            await _planRepository.UpdateAsync(plan, cancellationToken);
            _logger.LogInformation("Plan with id {Id} set to public", command.Id);
            return Result.Success();
        }
    }
}
