using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetAllPlanFeatures
{
    public class GetAllPlanFeaturesHandler: IRequestHandler<GetAllPlanFeaturesCommand, Result<List<GetAllPlanFeaturesResponse>>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public GetAllPlanFeaturesHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result<List<GetAllPlanFeaturesResponse>>> Handle(GetAllPlanFeaturesCommand command, CancellationToken cancellationToken)
        {
            var planFeatueList = await _planFeatureRepository.GetAllAsync();
            if (!planFeatueList.Any())
            {
                return Result<List<GetAllPlanFeaturesResponse>>.Failure("Planfeature list is empty");
            }

            var response = planFeatueList.Select(pf => new GetAllPlanFeaturesResponse
            {
                PlanId = pf.PlanId,
                Key = pf.Key,
                Name = pf.Name,
                Description = pf.Description,
                IsEnabled = pf.IsEnabled,
                Limit = pf.Limit,
                Unit = pf.Unit
            }).ToList();

            return Result<List<GetAllPlanFeaturesResponse>>.Success(response);
        }
    }
}
