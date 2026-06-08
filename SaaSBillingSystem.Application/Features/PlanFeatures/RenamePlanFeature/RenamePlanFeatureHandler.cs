using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.RenamePlanFeature
{
    public class RenamePlanFeatureHandler: IRequestHandler<RenamePlanFeatureCommand, Result<bool>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public RenamePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result<bool>> Handle(RenamePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if (planFeature == null)
            {
                return Result<bool>.Failure($"Planfeature with id {command.Id} was not found");
            }
            planFeature.Rename(command.Name, command.Description);
            await _planFeatureRepository.UpdateAsync(planFeature);
            return Result<bool>.Success(true);
        }
    }
}
