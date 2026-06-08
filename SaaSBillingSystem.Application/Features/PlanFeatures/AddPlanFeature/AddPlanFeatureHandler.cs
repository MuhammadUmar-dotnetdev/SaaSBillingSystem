using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.AddPlanFeature
{
    public class AddPlanFeatureHandler: IRequestHandler<AddPlanFeatureCommand, Result<AddPlanFeatureResponse>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        private readonly IPlanRepository _planRepository;
        public AddPlanFeatureHandler(IPlanFeatureRepository planFeatureRepository, IPlanRepository planRepository)
        {
            _planFeatureRepository = planFeatureRepository;
            _planRepository = planRepository;
        }

        public async Task<Result<AddPlanFeatureResponse>> Handle(AddPlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.PlanId);
            if (plan == null)
            {
                return Result<AddPlanFeatureResponse>.Failure($"Plan with id {command.PlanId} was not found");
            }

            if(await _planFeatureRepository.ExistsAsync(command.PlanId, command.Key))
            {
                return Result<AddPlanFeatureResponse>.Failure($"Feature '{command.Key}' already exists");
            }

            var planFeature = PlanFeature.Create(
                    command.PlanId,
                    command.Key,
                    command.Name,
                    command.Description,
                    command.IsEnabled,
                    command.Limit,
                    command.Unit
                );
            await _planFeatureRepository.AddAsync(planFeature);
            var response = new AddPlanFeatureResponse
            { 
                Id = planFeature.Id
            };
            return Result<AddPlanFeatureResponse>.Success(response);
        }
    }
}
