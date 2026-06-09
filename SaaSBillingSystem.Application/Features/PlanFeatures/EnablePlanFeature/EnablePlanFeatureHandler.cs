using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature
{
    public class EnablePlanFeatureHandler: IRequestHandler<EnablePlanFeatureCommand, Result>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public EnablePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result> Handle(EnablePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if(planFeature == null)
            {
                return Result.Failure($"Planfeature with id {command.Id} was not found");
            }
            planFeature.Enable();
            await _planFeatureRepository.UpdateAsync(planFeature);
            return Result.Success();
        }
    }
}
