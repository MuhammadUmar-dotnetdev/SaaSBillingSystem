using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.GetAllPlans
{
    public class GetAllPlansHandler: IRequestHandler<GetAllPlansCommand, Result<List<GetAllPlansResponse>>>
    {
        private readonly IPlanRepository _planRepository;
        public GetAllPlansHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result<List<GetAllPlansResponse>>> Handle(GetAllPlansCommand command, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetAllPlansAsync();
            if(!plans.Any())
            {
                return Result<List<GetAllPlansResponse>>.Failure("Plan list is empty");
            }

            var response = plans.Select(p => new GetAllPlansResponse
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                BillingCycle = p.BillingCycle,
                MaxUsers = p.MaxUsers,
                MaxProjects = p.MaxProjects,
                MaxStorageInMb = p.MaxStorageInMb,
                IsPublic = p.IsPublic,
            }).ToList();

            return Result<List<GetAllPlansResponse>>.Success(response);
        }
    }
}
