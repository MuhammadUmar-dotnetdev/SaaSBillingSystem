using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.FeaturesForPlan.GetByKey
{
    public class GetByKeyCommand: IRequest<Result<GetByKeyResponse>>
    {
        public string Key { get; private set; } = string.Empty;
        public GetByKeyCommand(string key)
        {
            Key = key;
        }
    }
}
