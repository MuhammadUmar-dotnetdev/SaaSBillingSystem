using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature
{
    public class EnablePlanFeatureHandler: IRequestHandler<EnablePlanFeatureCommand, Result<bool>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public EnablePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result<bool>> Handle(EnablePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if(planFeature == null)
            {
                return Result<bool>.Failure($"Planfeature with id {command.Id} was not found");
            }
            planFeature.Enable();
            await _planFeatureRepository.UpdateAsync(planFeature);
            return Result<bool>.Success(true);
        }
    }
}
