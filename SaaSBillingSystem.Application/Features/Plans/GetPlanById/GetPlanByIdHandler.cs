using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.GetPlanById
{
    public class GetPlanByIdHandler: IRequestHandler<GetPlanByIdCommand, Result<GetPlanByIdResponse>>
    {
        private readonly IPlanRepository _planRepository;
        public GetPlanByIdHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result<GetPlanByIdResponse>> Handle(GetPlanByIdCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if(plan == null)
            {
                return Result<GetPlanByIdResponse>.Failure($"Plan with {command.Id} not found");
            }

            var response = new GetPlanByIdResponse
            {
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                BillingCycle = plan.BillingCycle,
                MaxUsers = plan.MaxUsers,
                MaxProjects = plan.MaxProjects,
                MaxStorageInMb = plan.MaxStorageInMb,
                IsPublic = plan.IsPublic,
            };
            return Result<GetPlanByIdResponse>.Success(response);
        }
    }
}
