using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Infrastructure.Persistence.Configurations
{
    public class OrganizationMembershipConfiguration: IEntityTypeConfiguration<OrganizationMembership>
    {
        public void Configure(EntityTypeBuilder<OrganizationMembership> builder)
        {
            builder.ToTable("organization_memberships");

            builder.HasKey(x => new
            {
                x.UserId,
                x.OrganizationId
            });

            builder.HasOne(om => om.User)
                .WithMany(u => u.Memberships)
                .HasForeignKey(om => om.UserId);

            builder.HasOne(om => om.Organization)
                .WithMany(o => o.Memberships)
                .HasForeignKey(om => om.OrganizationId);

            builder.Property(om => om.Role)
                .IsRequired();
        }
    }
}
