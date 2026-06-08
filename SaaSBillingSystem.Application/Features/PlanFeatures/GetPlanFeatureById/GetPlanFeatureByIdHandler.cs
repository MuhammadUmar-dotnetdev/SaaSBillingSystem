using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetPlanFeatureById
{
    public class GetPlanFeatureByIdHandler: IRequestHandler<GetPlanFeatureByIdCommand, Result<GetPlanFeatureByIdResponse>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public GetPlanFeatureByIdHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result<GetPlanFeatureByIdResponse>> Handle(GetPlanFeatureByIdCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if (planFeature == null)
            {
                return Result<GetPlanFeatureByIdResponse>.Failure($"Plan with id {command.Id} was not found");
            }

            var response = new GetPlanFeatureByIdResponse
            {
                PlanId = planFeature.PlanId,
                Key = planFeature.Key,
                Name = planFeature.Name,
                Description = planFeature.Description,
                IsEnabled = planFeature.IsEnabled,
                Limit = planFeature.Limit,
                Unit = planFeature.Unit
            };

            return Result<GetPlanFeatureByIdResponse>.Success(response);
        }
    }
}
