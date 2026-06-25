using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Infrastructure.Persistence.Configurations
{
    public class PlanFeatureConfiguration : IEntityTypeConfiguration<PlanFeature>
    {
        public void Configure(EntityTypeBuilder<PlanFeature> builder)
        {
            builder.ToTable("plan_feature");

            builder.HasKey(pf => new
            {
                pf.PlanId,
                pf.FeatureId
            });

            builder.HasOne(pf => pf.Plan)
                .WithMany(p => p.PlanFeatures)
                .HasForeignKey(p => p.PlanId);

            builder.HasOne(pf => pf.Feature)
                .WithMany(f => f.PlanFeatures)
                .HasForeignKey(f => f.FeatureId);
        }
    }
}
