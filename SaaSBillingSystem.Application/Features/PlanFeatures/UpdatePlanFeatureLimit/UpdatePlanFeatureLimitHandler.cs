using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.UpdatePlanFeatureLimit
{
    public class UpdatePlanFeatureLimitHandler: IRequestHandler<UpdatePlanFeatureLimitCommand, Result<bool>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public UpdatePlanFeatureLimitHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result<bool>> Handle(UpdatePlanFeatureLimitCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if (planFeature == null)
            {
                return Result<bool>.Failure($"Planfeature with id {command.Id} was not found");
            }
            planFeature.UpdateLimit(command.Limit, command.Unit);
            await _planFeatureRepository.UpdateAsync(planFeature);
            return Result<bool>.Success(true);
        }
    }
}
