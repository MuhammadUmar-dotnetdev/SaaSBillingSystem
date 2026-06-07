using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.CreatePlan
{
    public class CreatePlanHandler: IRequestHandler<CreatePlanCommand, Result<CreatePlanResponse>>
    {
        private readonly IPlanRepository _planRepository;
        public CreatePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result<CreatePlanResponse>> Handle(CreatePlanCommand command, CancellationToken cancellationToken)
        {
            var newPlan = Plan.Create(command.Name, command.Description, command.Price, command.BillingCycle, command.MaxUsers,
                command.MaxProjects, command.MaxStorageInMb, command.IsPublic);

            await _planRepository.AddAsync(newPlan);

            var response = new CreatePlanResponse
            {
                Id = newPlan.Id,
                Name = command.Name,
            };
            return Result<CreatePlanResponse>.Success(response);
        }
    }
}
