using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.CreatePlan
{
    public class CreatePlanHandler: IRequestHandler<CreatePlanCommand, Result<CreatePlanResponse>>
    {
        private readonly IPlanRepository _planRepository;
        public readonly ILogger<CreatePlanHandler> _logger;
        public CreatePlanHandler(IPlanRepository planRepository, ILogger<CreatePlanHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<Result<CreatePlanResponse>> Handle(CreatePlanCommand command, CancellationToken cancellationToken)
        {
            var planExists = await _planRepository.ExistsByNameAndBillingCycleAsync(command.Name, command.BillingCycle, cancellationToken);
            if (planExists)
            {
                _logger.LogWarning("A plan with name {Name} and billing cycle {BillingCycle} already exists.", command.Name, command.BillingCycle.ToString());
                return Result<CreatePlanResponse>.Failure("A plan with this name and billing cycle already exists.");
            }
            var plan = Plan.Create(command.Name, command.Description, command.Price, command.BillingCycle, command.MaxUsers,
                command.MaxProjects, command.MaxStorageInMb, command.IsPublic);

            await _planRepository.AddAsync(plan);

            _logger.LogInformation("New plan created successfully with id {Id}", plan.Id);

            var response = new CreatePlanResponse
            {
                Id = plan.Id,
                Name = command.Name,
            };
            return Result<CreatePlanResponse>.Success(response);
        }
    }
}
