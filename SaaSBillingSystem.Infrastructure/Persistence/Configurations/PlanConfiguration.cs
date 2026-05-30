using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Infrastructure.Persistence.Configurations
{
    public class PlanConfiguration: IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(p => p.Id);


        }
    }
}
