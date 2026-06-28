namespace SaaSBillingSystem.Application.Features.FeaturesForPlan.GetByKey
{
    public class GetByKeyResponse
    {
        public Guid Id { get; private set; }
        public string Key { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public GetByKeyResponse(Guid id, string key, string name, string description)
        {
            Id = id;
            Key = key;
            Name = name;
            Description = description;
        }
    }
}
