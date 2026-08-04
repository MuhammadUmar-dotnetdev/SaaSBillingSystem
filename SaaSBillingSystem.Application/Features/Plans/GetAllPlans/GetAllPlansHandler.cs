using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.GetAllPlans
{
    public class GetAllPlansHandler: IRequestHandler<GetAllPlansCommand, Result<List<GetAllPlansResponse>>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<GetAllPlansHandler> _logger;
        public GetAllPlansHandler(IPlanRepository planRepository, ILogger<GetAllPlansHandler> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<Result<List<GetAllPlansResponse>>> Handle(GetAllPlansCommand command, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetAllPlansAsync();
            if(!plans.Any())
            {
                _logger.LogWarning("Plan list is empty");
                return Result<List<GetAllPlansResponse>>.Failure("Plan list is empty");
            }

            var response = plans.Select(p => new GetAllPlansResponse
            {
                Id = p.Id,
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
