using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.UpdatePlan
{
    public class UpdatePlanHandler: IRequestHandler<UpdatePlanCommand, Result<UpdatePlanResponse>>
    {
        private readonly IPlanRepository _planRepository;
        public UpdatePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result<UpdatePlanResponse>> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if(plan == null)
            {
                return Result<UpdatePlanResponse>.Failure($"Plan with id {command.Id} was not found");
            }

            plan.Update(command.Name, command.Description, command.Price, command.BillingCycle, command.MaxUsers, command.MaxProjects, command.MaxStorageInMb, command.IsPublic);

            await _planRepository.UpdateAsync(plan);
            var response = new UpdatePlanResponse
            {
                Id = plan.Id,
                Name = plan.Name
            };
            return Result<UpdatePlanResponse>.Success(response);
        }
    }
}
